namespace AguiaLeague.Domain.Models;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCadastro = DateTime.UtcNow;
        DataModificacao = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataModificacao { get; set; }
}