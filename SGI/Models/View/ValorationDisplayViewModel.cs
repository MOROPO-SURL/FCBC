namespace MOROPO.FCBC.SGI.Models.View
{
    public class ValorationDisplayViewModel
    {
        public System.Guid Id { get; set; }
        
        public CriticDisplayViewModel Critic { get; set; }

        public string Declaration { get; set; }

        public System.DateTime? DeclaredAt { get; set; }
    }
}
