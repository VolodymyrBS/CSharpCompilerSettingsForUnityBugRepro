using UnityEngine;

namespace TrueSync {

    /**
    *  @brief A deterministic version of Unity's Transform component for 2D physics. 
    **/
    [ExecuteInEditMode]
    public class TSTransform2D : MonoBehaviour {

        private const float DELTA_TIME_FACTOR = 10f;

        #if STORE_DATA
        [AddTracking] private string _trName;
        #endif
        
        [SerializeField]
        [AddTracking]
        private TSVector2 _position;
        
        /**
        *  @brief Property access to position. 
        *  
        *  It works as proxy to a Body's position when there is a collider attached.
        **/
        public TSVector2 position {
            get {
                if (tsCollider != null && tsCollider.Body != null) {
					return tsCollider.Body.TSPosition - scaledCenter;
                }

				return _position;
            }
            set {
                _position = value;

                if (tsCollider != null && tsCollider.Body != null) {
                    tsCollider.Body.TSPosition = _position + scaledCenter;
                }
            }
        }

        [SerializeField]
        [HideInInspector]
        private FP _rotation;

        /**
        *  @brief Property access to rotation. 
        *  
        *  It works as proxy to a Body's rotation when there is a collider attached.
        **/
        public FP rotation {
            get {
                if (tsCollider != null && tsCollider.Body != null) {
                    return tsCollider.Body.TSOrientation * FP.Rad2Deg;
                }

                return _rotation;
            }
            set {
                _rotation = value;

                if (tsCollider != null && tsCollider.Body != null) {
                    tsCollider.Body.TSOrientation = _rotation * FP.Deg2Rad;
                }
            }
        }

        [SerializeField]
        [HideInInspector]
        private TSVector _scale;

        /**
        *  @brief Property access to scale. 
        **/
        public TSVector scale {
            get {
                return _scale;
            }
            set {
                _scale = value;
            }
        }

        [SerializeField]
        [HideInInspector]
        private bool _serialized;

        private TSVector2 scaledCenter {
            get {
                if (tsCollider != null) {
                    return tsCollider.ScaledCenter;
                }

                return TSVector2.zero;
            }
        }

        [HideInInspector]
        public TSCollider2D tsCollider;

        [HideInInspector]
        public TSTransform2D tsParent;

        private bool initialized = false;

//		private TSRigidBody2D rb;

        public void Start() {
            if (!Application.isPlaying) {
                return;
            }

            Initialize();
//			rb = GetComponent<TSRigidBody2D> ();
        }

        /**
        *  @brief Initializes internal properties based on whether there is a {@link TSCollider2D} attached.
        **/
        public void Initialize() {
            if (initialized) {
                return;
            }

            tsCollider = GetComponent<TSCollider2D>();
            if (transform.parent != null) {
                tsParent = transform.parent.GetComponent<TSTransform2D>();
            }

            if (!_serialized) {
                UpdateEditMode();
            }

            if (tsCollider != null) {
                if (tsCollider.IsBodyInitialized) {
                    tsCollider.Body.TSPosition = _position + scaledCenter;
                    tsCollider.Body.TSOrientation = _rotation * FP.Deg2Rad;
                }
            }

            #if STORE_DATA
            _trName = name; 
            #endif
            
            initialized = true;
			position = _position;
			UpdatePlayMode();
        }

        public void Update() {
            if (Application.isPlaying) {
                if (initialized) {
                    UpdatePlayMode();
                }
            } else {
                UpdateEditMode();
            }
        }

        private void UpdateEditMode() {
            
        }
        
        [HideInInspector]
        public float ZOfffset;
        
        private void UpdatePlayMode() {
            
        }
    }

}