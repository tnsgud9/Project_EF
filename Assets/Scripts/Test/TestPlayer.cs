using Collections;
using Collections.DependencyInject;
using UnityEngine;

namespace Test
{
    public class TestPlayer : BaseBehaviour
    {
        [InjectComponent]
        private Rigidbody _rigidbody;

        [InjectComponent]
        private Collider _collider;

        void Start()
        {
            PrintStatus();
        }
        public void PrintStatus()
        {
            Debug.Log(_rigidbody != null ? "Rigidbody 성공적으로 주입됨!" : "Rigidbody 주입 실패!");
            Debug.Log(_collider != null ? "Collider 성공적으로 주입됨!" : "Collider 주입 실패!");
        }
    }
}