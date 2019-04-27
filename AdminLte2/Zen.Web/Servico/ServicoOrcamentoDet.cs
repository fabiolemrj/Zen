using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcamentoDet
    {
        public OrcamentoDet ObterObjetoPorId(ZenContext db, int idpedido, int item)
        {
           
             return  db.OrcamentoDets.SingleOrDefault(c => c.IdPedido == idpedido && c.Item == item);
           
        }

        public void Salvar(ZenContext db, OrcamentoDet objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item) == null)
            {
                objeto.Item = db.OrcamentoDets.Where(c=>c.IdPedido == objeto.IdPedido).Max(c => c.Item) + 1;
                db.OrcamentoDets.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, OrcamentoDet objeto)
        {
            db.OrcamentoDets.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<OrcamentoDet> ObterListaObjetos(ZenContext db, Orcamento orcamento)
        {
            return db.OrcamentoDets.Where(u => u.IdPedido == orcamento.IdPedido).OrderBy(u => u.Item);
        }


        public IQueryable<OrcamentoDet> ObterListaObjetos(ZenContext db, int idpedido)
        {
            var lst = new List<OrcamentoDet>();

            var q = (from orcdet in db.OrcamentoDets
                     join prod in db.Produtos on orcdet.IdProduto equals prod.Id
                     into _prod
                     from prod in _prod.DefaultIfEmpty()
                     join orc in db.Orcamentos on orcdet.IdPedido equals orc.IdPedido
                     into _orc
                     from orc in _orc.DefaultIfEmpty()
                     join mat1 in db.Materiais on orcdet.IdMaterial1 equals mat1.Id
                     into _mat1
                     from mat1 in _mat1.DefaultIfEmpty()
                     join mat2 in db.Materiais on orcdet.IdMaterial2 equals mat2.Id
                     into _mat2
                     from mat2 in _mat2.DefaultIfEmpty()
                     join mat3 in db.Materiais on orcdet.IdMaterial3 equals mat3.Id
                     into _mat3
                     from mat3 in _mat3.DefaultIfEmpty()
                     join mat4 in db.Materiais on orcdet.IdMaterial4 equals mat4.Id
                     into _mat4
                     from mat4 in _mat4.DefaultIfEmpty()
                     select new { orcdet, orc, prod,mat1,mat2,mat3,mat4 }).Where(c=>c.orcdet.IdPedido == idpedido).OrderBy(c=>c.orcdet.Item);

            foreach (var item in q)
            {
                lst.Add(new OrcamentoDet()
                {
                    IdPedido = item.orcdet.IdPedido,
                    AltHs = item.orcdet.AltHs,
                    AltRs = item.orcdet.AltRs,
                    CodFaca = item.orcdet.CodFaca,
                    Cola = item.orcdet.Cola,
                    ContraPlaca = item.orcdet.ContraPlaca,
                    Cordao = item.orcdet.Cordao,
                    CorteEsp = item.orcdet.CorteEsp,
                    CorteSimples = item.orcdet.CorteSimples,
                    CorteVinco = item.orcdet.CorteVinco,
                    DescMaterial1 = item.orcdet.DescMaterial1,
                    DescMaterial2 = item.orcdet.DescMaterial2,
                    DescMaterial3 = item.orcdet.DescMaterial3,
                    DescMaterial4 = item.orcdet.DescMaterial4,
                    DescProduto = item.orcdet.DescProduto,
                    Dobra = item.orcdet.Dobra,
                    Elastico = item.orcdet.Elastico,
                    Encadernacao = item.orcdet.Encadernacao,
                    Espiral = item.orcdet.Espiral,
                    Executado = item.orcdet.Executado,
                    HotStamp = item.orcdet.HotStamp,
                    HsChapa = item.orcdet.HsChapa,
                    IdMaterial1 = item.orcdet.IdMaterial1,
                    IdMaterial2 = item.orcdet.IdMaterial2,
                    IdMaterial3 = item.orcdet.IdMaterial3,
                    IdMaterial4 = item.orcdet.IdMaterial4,
                    IdOsi = item.orcdet.IdOsi,
                    IdProduto = item.orcdet.IdProduto,
                    Ilhos = item.orcdet.Ilhos,
                    Item = item.orcdet.Item,
                    LamFoscaF = item.orcdet.LamFoscaF,
                    LamFoscaV = item.orcdet.LamFoscaV,
                    LargHs = item.orcdet.LargHs,
                    LargRs = item.orcdet.LargRs,
                    Mat1Fornec = item.orcdet.Mat1Fornec,
                    Mat2Fornec = item.orcdet.Mat2Fornec,
                    Mat3Fornec = item.orcdet.Mat3Fornec,
                    Mat4Fornec = item.orcdet.Mat4Fornec,
                    Material1 = item.mat1,
                    Material2 = item.mat2,
                    Material3 = item.mat3,
                    Material4 = item.mat4,
                    MeioCorte = item.orcdet.MeioCorte,
                    Montagem = item.orcdet.Montagem,
                    ObsAcab1 = item.orcdet.ObsAcab1,
                    ObsImp = item.orcdet.ObsImp,
                    OffF = item.orcdet.OffF,
                    OffSet = item.orcdet.OffSet,
                    OffV = item.orcdet.OffV,
                    OutrosAcab1 = item.orcdet.OutrosAcab1,
                    OutrosAcab2 = item.orcdet.OutrosAcab2,
                    OutrosImp = item.orcdet.OutrosImp,
                    Pintura = item.orcdet.Pintura,
                    Prazo = item.orcdet.Prazo,
                    Produto = item.prod,
                    Quant = item.orcdet.Quant,
                    RelevoSeco = item.orcdet.RelevoSeco,
                    RsChapa = item.orcdet.RsChapa,
                    SemAcab = item.orcdet.SemAcab,
                    Vazador = item.orcdet.Vazador,
                    Vinco = item.orcdet.Vinco,
                    WireO = item.orcdet.WireO
                });
            }

            return lst.AsQueryable();
            //return db.OrcamentoDets.Where(u => u.IdPedido == idpedido).OrderBy(u => u.Item);
        }


    }
}