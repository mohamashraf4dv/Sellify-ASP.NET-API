namespace Sellify.Domain.Contracts
{
    public interface IEntityUpdatable
    {
        public DateTime? LastUpdatedAt { get; set; }

    }
}
