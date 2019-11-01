﻿using UnityEngine;
using UnityEngine.Assertions;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidsFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _boidsParent;

        [SerializeField]
        private GameObject _boidPrototype;

        public (Agent agent, AISubsystem subsystem) CreateNewBoid()
        {
            var newBoid = Instantiate(_boidPrototype, new Vector3(Random.value * 10, Random.value * 5, 0), Quaternion.identity, _boidsParent);
            var agent = newBoid.GetComponentInChildren<Agent>();
            agent.Prediction = new SteeringPrediction()
            {
                Velocity = RandomVector2()
            };

            var subsystem = newBoid.GetComponentInChildren<AISubsystem>();
            return (agent, subsystem);
        }

        private Vector2 RandomVector2()
        {
            return new Vector2(Random.value, Random.value);
        }

        private void OnValidate()
        {
            if (_boidPrototype != null
                && !_boidPrototype.GetComponentInChildren<Agent>()
                && !_boidPrototype.GetComponentInChildren<AISubsystem>())
            {
                _boidPrototype = null;
            }

            Assert.IsNotNull(_boidPrototype);
        }
    }
}