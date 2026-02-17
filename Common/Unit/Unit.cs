using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /*
    public int[] _Damage;
    public float[] _AttackRange;  // 공격 사거리
    public float[] _Speed;
    public float[] _AttackSpeed;
    */

    public int Damage;
    public float AttackRange;  // 공격 사거리
    public float Speed;
    public float AttackSpeed;

    public int IncHp;
    public int IncDam;
    public float DecAttSp;

    GameObject effect;
    public GameObject Projectile;   // 원거리 투사체
    public Transform ShootPos;  // 발사 위치

    private bool Fighting;  // 근접 공격 상태
    private bool Shooting;  // 원거리 공격 상태

    private GameObject[] Other; // 아군, 적 감지
    private string OtherTag;

    Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashWalk = Animator.StringToHash("Walk");
    private float speed_animation = 1f;


    [System.Serializable]
    public enum Type
    {
        Shooter,
        Fighter
    }

    public AudioClip attackSound;

    public Type Class;

    void Start()
    {
        if (this.gameObject.tag == "Friendly" && this.gameObject.layer == 13)   // 상대방 구분
        {
            OtherTag = "Enemy";
            Damage += (int)PlayerPrefs.GetFloat("unitDamage");
            this.gameObject.GetComponent<UnitHp>().Hp += (int)PlayerPrefs.GetFloat("unitHp");
        }
        else
        { 
            OtherTag = "Friendly";
            for (int i = 1; i < PlayerPrefs.GetInt("level"); i++)
            {
                Damage += IncDam;
                this.gameObject.GetComponent<UnitHp>().Hp += IncHp;
                AttackSpeed -= DecAttSp;
            }
        }

        FindObj();
        //rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effect = this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;
        /*
        if (transform.childCount > 2)
            transform.GetChild(2).gameObject.SetActive(false);
            */
    }

    void FindObj()
    {
        Other = GameObject.FindGameObjectsWithTag(OtherTag);
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            FindObj();
            float closestDist = Mathf.Infinity;
            GameObject closestEn = null;
            GameObject[] allEn = Other;

            foreach (GameObject potentTarget in allEn) // 가장 가까운 적 공격
            {
                float distTarget;// = Mathf.Abs(potentTarget.transform.position.x - transform.position.x);
                                 //float distTarget = (potentTarget.transform.position - this.transform.position).sqrMagnitude;

                try
                {
                    distTarget = Mathf.Abs(potentTarget.transform.position.x - transform.position.x);
                }
                catch (MissingReferenceException)
                {
                    return;
                }

                if (distTarget < closestDist && potentTarget.layer != 9
                && ((this.gameObject.tag == "Friendly" && potentTarget.transform.position.x > this.transform.position.x) || (this.gameObject.tag == "Enemy" && potentTarget.transform.position.x < this.transform.position.x))) // 적이 뒤로 넘어가면 공격 안함
                {
                    closestDist = distTarget;
                    closestEn = potentTarget;
                }
            }
            if (Class == Type.Shooter && AttackRange + closestEn.transform.localScale.x > closestDist && !Shooting) { StartCoroutine(Shoot(closestEn)); } //&& FightDis < closestDist) 
            else if (Class == Type.Fighter && AttackRange + closestEn.transform.localScale.x > closestDist && !Fighting) { StartCoroutine(Fight(closestEn)); }
            else if (!Shooting && !Fighting) { Move(); }

        }

    }

    void Move()
    {
        //rb.velocity = transform.right * Speed;

        if (this.gameObject.tag == "Friendly")
            transform.Translate(transform.right * Speed * Time.deltaTime);
        else
            transform.Translate(-transform.right * Speed * Time.deltaTime);

        animator.SetBool(hashWalk, true);
    }

    IEnumerator Shoot(GameObject Enemy)
    {
        

        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        animator.speed = speed_animation / AttackSpeed;
        Shooting = true;
        ShootPos.LookAt(Enemy.transform);

        if (Enemy != null && Enemy.activeSelf)
        {
            yield return new WaitForSeconds(AttackSpeed);

            GameObject projectile = Projectile;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (!gameObject.transform.GetChild(i).gameObject.activeSelf)
                {
                    projectile = this.gameObject.transform.GetChild(i).gameObject;
                    projectile.transform.position = ShootPos.position;
                    projectile.transform.rotation = ShootPos.rotation;
                    projectile.tag = OtherTag;
                    projectile.name = Damage.ToString();
                    projectile.SetActive(true);
                    break;
                }
                else if (i + 1 == transform.childCount)
                {
                    projectile = Instantiate(Projectile, ShootPos.position, ShootPos.rotation);
                    projectile.transform.SetParent(gameObject.transform);
                    projectile.tag = OtherTag;
                    projectile.name = Damage.ToString();
                    break;
                }
            }

            animator.SetBool(hashAttack, false);
            yield return new WaitForSeconds(AttackSpeed);
            animator.speed = 1;
        }
        else
        {
            animator.SetBool(hashAttack, false);
        }
        Shooting = false;

    }

    IEnumerator Fight(GameObject Enemy)
    {
        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        animator.speed = speed_animation / AttackSpeed / 2;
        Fighting = true;
        yield return new WaitForSeconds(AttackSpeed);
        if (Enemy != null && Enemy.activeSelf && Mathf.Abs(Enemy.transform.position.x - transform.position.x) <= AttackRange + Enemy.transform.localScale.x)
        {
            Enemy.GetComponent<UnitHp>().Damaged(Damage, "Sword");
            ShowEffect(Enemy);
            animator.SetBool(hashAttack, false);
            SoundManager.instance.PlaySE(attackSound);
            yield return new WaitForSeconds(AttackSpeed);
            effect.SetActive(false);
            animator.speed = 1;
        }
        else
        {
            animator.SetBool(hashAttack, false);
        }
        Fighting = false;
    }

    void ShowEffect(GameObject Enemy)
    {
        // effect = this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;
        effect.transform.localScale = new Vector3(2, 2, 2);
        effect.transform.position = Enemy.transform.position;
        effect.SetActive(true);
    }

    public void Upgrade(int per)
    {
        Debug.Log("Upgrade");
        Damage += Damage * per / 100;
        Debug.Log(Damage);
        //AttackSpeed -= AttackSpeed * per / 100;
        Speed += Speed * per / 100;
    }

    private void OnDisable()
    {
        Fighting = false;
        Shooting = false;
    }
}
