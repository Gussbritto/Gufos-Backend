using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Domains;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories {
        public class TipoUsuarioRepository : ITipoUsuario {
                public async Task<TipoUsuario> Alterar (TipoUsuario tipousuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.Entry (tipousuario).State = EntityState.Modified;
                        await _contexto.SaveChangesAsync ();
                        return tipousuario;
                    }
                }

                public async Task<TipoUsuario> BuscarPorID (int id) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.TipoUsuario.FindAsync (id);
                    }
                }

                public async Task<TipoUsuario> Excluir (TipoUsuario tipousuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.TipoUsuario.Remove (tipousuario);
                        await _contexto.SaveChangesAsync ();
                        return tipousuario;

                    }

                }

                public async Task<List<TipoUsuario>> Listar () {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.TipoUsuario.ToListAsync ();
                    }
                }

                public async Task<TipoUsuario> Salvar (TipoUsuario tipousuario) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        await _contexto.AddAsync (tipousuario);
                        await _contexto.SaveChangesAsync ();
                        return tipousuario;

                    }

                }
        }
}