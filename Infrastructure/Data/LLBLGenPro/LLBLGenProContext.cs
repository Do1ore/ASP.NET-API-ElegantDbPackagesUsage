using Domain.Entities;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Infrastructure.Data.LLBLGenPro;

public class LlblGenProContext : Context
{
    public LlblGenProContext(bool setExistingEntityFieldsInGet) : base(setExistingEntityFieldsInGet)
    {
    }

    public IEntity Entity { get; set; }
    public void zxc()
    {
        
    }
}