using System;
using System.Collections;

namespace Entities.Weapons
{
    public class Mine : Bomb
    {
        protected override void BeforeExplode()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerator Explode()
        {
            throw new NotImplementedException();
        }

        protected override void AfterExplode()
        {
            throw new NotImplementedException();
        }
    }
}