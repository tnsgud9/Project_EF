using UnityEngine;

namespace Entities.Weapons
{
    public class Dynamite : Bomb
    {
        protected override void Explode()
        {
            // 폭발 후 데미지 적용
            HandleExplosionComplete();
        }
    }
}