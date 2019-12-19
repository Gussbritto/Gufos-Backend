using System.Collections.Generic;
using BACKEND.Domains;
using System.Threading.Tasks;

namespace BACKEND.Interfaces
{
    public interface ICategoria
    {
          Task<List<Categoria>> Listar();
          Task<Categoria> BuscarPorID (int id);
          Task<Categoria> Salvar (Categoria categoria); 
          Task<Categoria> Alterar (Categoria categoria); 
          Task<Categoria> Excluir (Categoria categoria); 
    }
}