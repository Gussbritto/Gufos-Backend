using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Domains;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories {
        public class UsuarioRepository : IUsuario {
                public async Task<Usuario> Alterar (Usuario usuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.Entry (usuario).State = EntityState.Modified;
                        await _contexto.SaveChangesAsync ();
                        return usuario;
                    }
                }

                public async Task<Usuario> BuscarPorID (int id) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.Usuario.FindAsync (id);
                    }
                }

                public async Task<Usuario> Excluir (Usuario usuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.Usuario.Remove (usuario);
                        await _contexto.SaveChangesAsync ();
                        return usuario;

                    }

                }

                public async Task<List<Usuario>> Listar () {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.Usuario.ToListAsync ();
                    }
                }

                public async Task<Usuario> Salvar (Usuario usuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        await _contexto.AddAsync (usuario);
                        await _contexto.SaveChangesAsync ();
                        return usuario;

                    }

                }
        }
}