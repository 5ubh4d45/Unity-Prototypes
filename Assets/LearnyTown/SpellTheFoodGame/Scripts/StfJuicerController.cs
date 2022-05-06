using System;
using System.Collections.Generic;
using LiquidVolumeFX;
using UnityEngine;
using DG.Tweening;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfJuicerController : MonoBehaviour
    {

        [SerializeField] private LiquidVolume _liquidVolume;
        [SerializeField] private float _liquidRiseSpeed;
        
        
        [Space]
        [SerializeField] public float _liquidLevel;
        [SerializeField] private Color _liquidColor1;
        [SerializeField] private Color _liquidColor2;
        [SerializeField] private Color _liquidEmissionColor;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _liquidVolume.level = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            // UpdateLiquidLevel(_liquidLevel);
            // UpdateLiquidColor(_liquidColor1, _liquidColor2, _liquidEmissionColor);
            
        }

        public void UpdateJuicer(StfJuicerData juicerData)
        {
            UpdateLiquidLevel(juicerData.JuicerLevel);
            UpdateLiquidColor(juicerData.Color1, juicerData.Color2, juicerData.EmissionColor);
        }
        
        private void UpdateLiquidLevel(float level)
        {
            var targetLevel = Mathf.Clamp(level, 0, 1f);
            var tempLevel = _liquidVolume.level;

            DOTween.To(() => tempLevel, x => tempLevel = x, targetLevel, _liquidRiseSpeed)
                .OnUpdate(() => _liquidVolume.level = tempLevel);

        }

        private void UpdateLiquidColor(Color liquidColor1, Color liquidColor2, Color liquiEmissionColor)
        {

            _liquidVolume.liquidColor1 = liquidColor1;
            _liquidVolume.liquidColor2 = liquidColor2;
            _liquidVolume.emissionColor = liquiEmissionColor;
            
        }
    }

    [Serializable]
    public class StfJuicerData
    {
        public float JuicerLevel;
        public Color Color1;
        public Color Color2;
        public Color EmissionColor;
    }
}
