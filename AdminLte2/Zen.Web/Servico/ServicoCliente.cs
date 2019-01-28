using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;
using X.PagedList;

namespace Zen.Web.Servico
{
    public class ServicoCliente
    {
        public IEnumerable<Models.Cliente> ObterListaObjetos(ZenContext db, string filtroNome, bool ehDesigner, int tpfiltro)
        {
            var lst = new List<Cliente>();
            switch (tpfiltro)
            {
                case 1:
                    if (!ehDesigner)
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.OrderBy(u => u.Nome).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.Where(u => u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Nome.Contains(filtroNome) && u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                    }

                    break;

                case 2:
                    if (!ehDesigner)
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.OrderBy(u => u.Contato).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Contato.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.Where(u => u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Contato.Contains(filtroNome) && u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                    }
                    break;
                case 3:
                    if (!ehDesigner)
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.OrderBy(u => u.Tel1).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Tel1.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.CLientes.Where(u => u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                        else
                            lst = db.CLientes.Where(u => u.Tel1.Contains(filtroNome) && u.EhDesign == "S").OrderBy(u => u.Nome).ToList();
                    }
                    break;
            }

            return lst;
        }

        public IEnumerable<Models.Cliente> ObterListaObjetosPorNome(ZenContext db, string filtroNome, bool ehDesigner)
        {         

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                if(!ehDesigner)
                    return db.CLientes.OrderBy(u => u.Nome);
               else
                    return db.CLientes.Where(u => u.EhDesign == "S").OrderBy(u => u.Nome);
            }
            else
            {
                if (!ehDesigner)
                    return db.CLientes.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
                else
                    return db.CLientes.Where(u => u.Nome.Contains(filtroNome) && u.EhDesign == "S").OrderBy(u => u.Nome);
            }

        }

        public IEnumerable<Cliente> ObterObjetoPorApelido(ZenContext db, string apelido, bool ehdesign)
        {
            
            if (string.IsNullOrWhiteSpace(apelido))
            {
                if (!ehdesign)
                    return db.CLientes.OrderBy(u => u.Apelido == apelido);
                else
                    return db.CLientes.Where(u=>u.EhDesign == "S").OrderBy(u => u.Apelido == apelido);
            }
            else
            {
                if (!ehdesign)
                    return db.CLientes.Where(u => u.Apelido.Contains(apelido)).OrderBy(u => u.Nome);
                else
                    return db.CLientes.Where(u => u.Apelido.Contains(apelido) && u.EhDesign == "S").OrderBy(u => u.Nome);
            }
        }

        public Cliente ObterObjetoPorId(ZenContext db, int idcliente)
        {
            return db.CLientes.Find(idcliente);
        }

        public void Salvar(ZenContext db, Cliente cliente)
        {
            if (ObterObjetoPorId(db, cliente.IdCliente) == null)
            {
                cliente.IdCliente = db.CLientes.Max(c => c.IdCliente) + 1;
                db.CLientes.Add(cliente);
                
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Cliente cliente)
        {
            db.CLientes.Remove(cliente);
            db.SaveChanges();
        }
    }
}