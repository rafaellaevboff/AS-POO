using Domain.Entities;

namespace Domain.ViewModels
{
    public class MotoristaCreate
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int VeiculoID { get; set; }
    }
}