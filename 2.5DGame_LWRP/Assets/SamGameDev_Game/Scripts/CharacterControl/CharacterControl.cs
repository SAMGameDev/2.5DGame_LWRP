using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        Running,
        TransitionIndex,
        RunningTurn180,
    }

    public enum PlayableCharacterType
    {
        Girl,
        Ybot,
    }

    public class CharacterControl : MonoBehaviour
    {

        // public Material material;

        public Animator SkinnedMeshedAnimator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool MoveUp;
        public bool MoveDown;
        public bool Jump;
        public bool Attack;
        public bool Running;
        public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        //AI Attack Temp
        public DamageDetector damageDetector;

        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public List<Collider> RagdollParts = new List<Collider>();

        // public List<Collider> CollidingParts = new List<Collider>();

        public float gravityMultiplier;
        public float pullMultiplier;

        private List<TriggerDector> TriggerDectors = new List<TriggerDector>();

        private Rigidbody rigid;



        public Rigidbody RIGIBODY
        {

            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            bool switchback = false;

            if (!isFacingForward())
            {
                switchback = true;
            }

            FaceForward(true);

            // SetRagdollParts();
            SetColliderSpheres();


            if (switchback)
            {
                FaceForward(false);
            }

            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDetector>();


            RegisterCharacter();
        }


        public void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public List<TriggerDector> GetAllTriggers()
        {
            if (TriggerDectors.Count == 0)
            {
                TriggerDector[] arr = this.gameObject.GetComponentsInChildren<TriggerDector>();

                foreach (TriggerDector d in arr)
                {
                    TriggerDectors.Add(d);
                }
            }
            return TriggerDectors;
        }
        /*   private IEnumerator Start()
           {
               yield return new WaitForSeconds(5f);
               RIGIBODY.AddForce(200f * Vector3.up);
               yield return new WaitForSeconds(0.5f);
               TurnOnRagdoll();
           } */
        public void SetRagdollParts()
        {
            RagdollParts.Clear();

            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);

                    if (c.GetComponent<TriggerDector>() == null)
                    {
                        c.gameObject.AddComponent<TriggerDector>();
                    }
                }
            }
        }

        public void TurnOnRagdoll()
        {
            RIGIBODY.useGravity = false;
            RIGIBODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshedAnimator.enabled = false;
            SkinnedMeshedAnimator.avatar = null;

            foreach (Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        public void SetColliderSpheres()
        {
           BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.size.y / 2;
            float top = box.bounds.center.y + box.bounds.size.y / 2;
            float front = box.bounds.center.z + box.bounds.size.z / 2;
            float back = box.bounds.center.z - box.bounds.size.z / 2;


            GameObject bottomFrontHor = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomFrontVer = CreateEdgeSphere(new Vector3(0f, bottom + 0.05f, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

            bottomFrontHor.transform.parent = this.transform;
            bottomFrontVer.transform.parent = this.transform;
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFrontHor);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(bottomFrontVer);
            FrontSpheres.Add(topFront);

            float horizontalsection = (bottomFrontHor.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomFrontHor, -this.transform.forward, horizontalsection, 4, BottomSpheres);

            float verticalSection = (bottomFrontVer.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(bottomFrontVer, this.transform.up, verticalSection, 9, FrontSpheres);
        }

        private void FixedUpdate()
        {
            if (RIGIBODY.velocity.y < 0f)
            {
                RIGIBODY.velocity += (-Vector3.up * gravityMultiplier);
            }
            if (RIGIBODY.velocity.y > 0f && !Jump)
            {
                RIGIBODY.velocity += (-Vector3.up * pullMultiplier);
            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, float interations, List<GameObject> spheresList)
        {
            for (int i = 0; i < interations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1));

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                spheresList.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }

        public void MoveForward(float speed, float speedGraph)
        {
            transform.Translate(Vector3.forward * speed * speedGraph * Time.deltaTime);
        }

        public void FaceForward(bool forward)
        {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        public bool isFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* public void ChangerMaterial()
         {

             if(material == null)
             {
                 Debug.LogError("No Material Specified");
             }
             Renderer[] arr_materials = this.gameObject.GetComponentsInChildren<Renderer>();


             foreach (Renderer r in arr_materials)
             {
                 if (r.gameObject != this.gameObject)
                 {
                     r.material = material;
                 }

             }
         }*/



    }
}

