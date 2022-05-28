using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);

    void TakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0);
}
