namespace RentCarServer.Domain.Abstractions;

public abstract class Entity
{

    protected Entity()
    {
        Id = new IdentityId(Guid.CreateVersion7());  // crete sortable guid
        Guid id = Id;
        string idString = Id;
        isActive = true;

    }
    public IdentityId Id { get; private set; }
    public bool isActive { get; private set; }


    public DateTimeOffset CreatedAt { get;private set;  }
    public IdentityId CreatedBy { get; private set; } = default!;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IdentityId? UpdatedBy { get; private set; } 
    public bool IsDeleted { get; private set; } // soft delete flag
    public DateTimeOffset? DeletedAt { get; private set; }
    public IdentityId? DeletedBy { get; private set; }

    public void SetStatus(bool isActive)
    {
        this.isActive = isActive;
    }

    public void Delete()
    {
        IsDeleted = true;
     
    }

}

public sealed record IdentityId(Guid value)
{
    public static implicit operator Guid(IdentityId id) => id.value;
    public static implicit operator string(IdentityId id) => id.value.ToString();
}
