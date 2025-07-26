using Engine;
using UnityEngine;

namespace Entity.Entities.Flag
{
    public interface IConvertable
    {
        public bool IsConverted { get; set; }
        public TickActionBehaviour ConvertingBehaviour();
        public void Convert();
    }
}