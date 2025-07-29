using Engine;

namespace Entity.Entities.Flag
{
    public interface IConvertable
    {
        public bool IsConverted { get; set; }
        public TickActionBehaviour ConvertingBehaviour();
        public void Initialize(IStorage workerStorage);
        public void Convert();
    }
}