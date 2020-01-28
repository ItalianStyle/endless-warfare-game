using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class Tank : MonoBehaviour
    {
        #region Variables

        private Hull hull;
        private Turret turret;

        #endregion

        #region Built-In Method

        private void Awake()
        {
            hull = GetComponentInChildren<Hull>();
            turret = GetComponentInChildren<Turret>();
        }

        #endregion  

        #region Custom Methods
        public void TakeDamage(int damage)
        {
            hull.currHp -= damage;
            if (hull.currHp <= 0)
                Die();
        }

        void Die()
        {

        }

        public void RotateTank(Quaternion rotate)
        {
            hull.transform.Rotate(rotate.eulerAngles * Time.deltaTime);
            turret.transform.Rotate(rotate.eulerAngles * Time.deltaTime);
        }
        #endregion
    }
}
