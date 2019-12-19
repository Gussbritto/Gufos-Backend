using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Domains;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories {
        public class PresencaRepository : IPresenca {
                public async Task<Presenca> Alterar (Presenca presenca) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.Entry (presenca).State = EntityState.Modified;
                        await _contexto.SaveChangesAsync ();
                        return presenca;
                    }
                }

                public async Task<Presenca> BuscarPorID (int id) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.Presenca.FindAsync (id);
                    }
                }

                public async Task<Presenca> Excluir (Presenca presenca) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        _contexto.Presenca.Remove (presenca);
                        await _contexto.SaveChangesAsync ();
                        return presenca;

                    }

                }

                public async Task<List<Presenca>> Listar () {
                    using (GufosContext _contexto = new GufosContext ()) {
                        return await _contexto.Presenca.ToListAsync ();
                    }
                }

                public async Task<Presenca> Salvar (Presenca presenca) {
                    using (GufosContext _contexto = new GufosContext ()) {
                        await _contexto.AddAsync (presenca);
                        await _contexto.SaveChangesAsync ();
                        return presenca;

                    }

                }
        }
}