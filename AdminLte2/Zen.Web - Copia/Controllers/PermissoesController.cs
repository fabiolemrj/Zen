using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.PermissoesViewModel;

namespace Zen.Web.Controllers
{
    public class PermissoesController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoPerfil servicoPerfil = new ServicoPerfil();
        ServicoUsuarios servicoUsuario = new ServicoUsuarios();
        ServicoPerfilAcoes servico = new ServicoPerfilAcoes();
        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-users'> </i>
                           <a href='/Permissoes'> Permissões </a>
                         </li>
                         
                    </ol>";
        }

        private string CreateBreadCrumbCreatEdit()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>
                           <i class='fa fa-wrench'> </i>
                           <a href='/Configuracao'> Configuração </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: Permissoes
        [DireitoAcesso(Constantes.AC_PERFIL_CAD)]
        public ActionResult Index(int id)
        {
            var perfil = servicoPerfil.ObterObjetoPorId(db, id);
            var model = new IndexViewModel();
            model.idPerfil = perfil.Id;
            model.NmUsuario = perfil.Descricao;
            model.LstPermAbaCadastro = ObterLstPermAbaCadastro(perfil.Id);
            model.LstPermAbaTesouraria = ObterLstPermAbaTesouraria(perfil.Id);
            model.LstPermAbaTabAux = ObterLstPermAbaTabAux(perfil.Id);
            model.LstPermAbaTabOrcamento = ObterLstPermAbaTabOrcamento(perfil.Id);
            model.LstPermAbaTabRel = ObterLstPermAbaTabRel(perfil.Id);
            model.LstPermAbaTabProducao = ObterLstPermAbaTabProducao(perfil.Id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            var lst = new List<PerfilAcoes>();
            foreach (var item in model.LstPermAbaCadastro)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            foreach (var item in model.LstPermAbaTabAux)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            foreach (var item in model.LstPermAbaTabOrcamento)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            foreach (var item in model.LstPermAbaTabProducao)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            foreach (var item in model.LstPermAbaTabRel)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            foreach (var item in model.LstPermAbaTesouraria)
            {
                if (item.Selecionado)
                {
                    lst.Add(new PerfilAcoes()
                    {
                        Acao = item.Id,
                        IdPerfil = model.idPerfil
                    });
                }
            }

            servico.Salvar(db, lst, model.idPerfil);

            return RedirectToAction("Index", "Perfil");
        }


        private bool SelecaoPerfil(int idPerfil, int acao)
        {

            return servico.ObterObjetoPorId(db, idPerfil, acao) != null;
        }

        public List<Permissoes> ObterLstPermAbaTabProducao(int idPerfil)
        {
            var lista = new List<Permissoes>();

            lista.Add(ObterPermissao(Constantes.AC_PRD_EDIT_ACAB, "O.S.I. - Editar Especificações para Acabamento", SelecaoPerfil(idPerfil, Constantes.AC_PRD_EDIT_ACAB)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_EDIT_TINTA, "O.S.I. - Editar Especificações para Tinta", SelecaoPerfil(idPerfil, Constantes.AC_PRD_EDIT_TINTA)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_EDIT_MATRIZ, "O.S.I. - Editar Especificações para Matriz", SelecaoPerfil(idPerfil, Constantes.AC_PRD_EDIT_MATRIZ)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_EDIT_IMP, "O.S.I. - Editar Especificações para Impressão(Assinaturas/Variação)", SelecaoPerfil(idPerfil, Constantes.AC_PRD_EDIT_IMP)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_PRD_OSI, "Cronograma - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_PRD_OSI)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_ART, "Cronograma - Editar coluna ART", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_ART)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_FILM, "Cronograma - Editar coluna FILM", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_FILM)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_MAT, "Cronograma - Editar coluna MAT", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_MAT)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_FACA, "Cronograma - Editar coluna S.T.", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_FACA)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_PREVIA, "Cronograma - Editar coluna PREVIA", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_PREVIA)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_ACAB, "Cronograma - Editar coluna ACAB (Concluir O.S.I.)", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_ACAB)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_VER_ACAB, "Listagem de Acabamento - Visualizar", SelecaoPerfil(idPerfil, Constantes.AC_PRD_VER_ACAB)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_CORT, "Listagem de Acabamento - Concluir corte", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_CORT)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_VER_TINTA, "Listagem de Tintas - Visualizar", SelecaoPerfil(idPerfil, Constantes.AC_PRD_VER_TINTA)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_TINT, "Listagem de Tintas - Concluir tinta", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_TINT)));
            lista.Add(ObterPermissao(Constantes.AC_PRD_VER_MATRIZ, "Listagem de Matrizes - Visualizar", SelecaoPerfil(idPerfil, Constantes.AC_PRD_VER_MATRIZ)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_MATR, "Listagem de Matrizes - Concluir matriz", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_MATR)));
            lista.Add(ObterPermissao(Constantes.AC_CONS_PRD_DIA, "Produção Diária - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_PRD_DIA)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_DIA, "Produção Diária - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_DIA)));
            lista.Add(ObterPermissao(Constantes.AC_FINA_PRD_DIA, "Produção Diária - Finalizar uma OSI", SelecaoPerfil(idPerfil, Constantes.AC_FINA_PRD_DIA)));
            lista.Add(ObterPermissao(Constantes.AC_MOVER_LOTE, "Produção Diária - Mover uma OSI entre máquinas", SelecaoPerfil(idPerfil, Constantes.AC_MOVER_LOTE)));
            lista.Add(ObterPermissao(Constantes.AC_QUEBRAR_LOTE, "Produção Diária - Quebrar uma OSI", SelecaoPerfil(idPerfil, Constantes.AC_QUEBRAR_LOTE)));
            lista.Add(ObterPermissao(Constantes.AC_CONS_PRD_EXP, "Expedição - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_PRD_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_PRD_EXP, "Expedição - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_PRD_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_INC_PRD_EXP, "Expedição - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_INC_PRD_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_APG_PRD_EXP, "Expedição - Editar", SelecaoPerfil(idPerfil, Constantes.AC_APG_PRD_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_DAD_EXP, "Expedição - Editar Dados Expedicao", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_DAD_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_DESC_EXP, "Expedição - Conceder Descontos Expedicao", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_DESC_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_MOD_CLI_EXP, "Expedição - Modificar cliente original da OSI", SelecaoPerfil(idPerfil, Constantes.AC_MOD_CLI_EXP)));
            lista.Add(ObterPermissao(Constantes.AC_VIS_ENT_EXP_PEND, "Expedição - Visualizar Entregas Pendentes", SelecaoPerfil(idPerfil, Constantes.AC_VIS_ENT_EXP_PEND)));
            lista.Add(ObterPermissao(Constantes.AC_LISTA_PRODDIARIA, "Produção Diária - Visualizar Lista de Produção", SelecaoPerfil(idPerfil, Constantes.AC_LISTA_PRODDIARIA)));
            lista.Add(ObterPermissao(Constantes.AC_INC_OSE, "OSE - Incluir", SelecaoPerfil(idPerfil, Constantes.AC_INC_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_OSE, "OSE - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_CONS_OSE, "OSE - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_APG_OSE, "OSE - Excluir", SelecaoPerfil(idPerfil, Constantes.AC_APG_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_VALOR_OSE, "OSE - Editar valor Contas Pagar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_VALOR_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_VALORPREV_OSE, "OSE - Editar valor Previsto", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_VALORPREV_OSE)));
            lista.Add(ObterPermissao(Constantes.AC_CANCEL_OSE, "OSE - Cancelar", SelecaoPerfil(idPerfil, Constantes.AC_CANCEL_OSE)));

            return lista;
        }
        public List<Permissoes> ObterLstPermAbaTabOrcamento(int idPerfil)
        {
            var lista = new List<Permissoes>();
            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_PEDORC, "Pedido de Orçamento - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_PEDORC, "Pedido de Orçamento - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_PEDORC, "Pedido de Orçamento - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_PEDORC, "Pedido de Orçamento - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_PEDORC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_PEDORC, "Pedido de Orçamento - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_PEDORC, "Pedido de Orçamento - Editar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_PEDORC, "Pedido de Orçamento - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_PEDORC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_PEDORC, "Pedido de Orçamento - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_PEDORC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_ORC, "Orçamento - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_ORC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_ORC, "Orçamento - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_ORC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_ORC, "Orçamento - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_ORC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_ORC, "Orçamento - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_ORC)));
            lista.Add(ObterPermissao(Constantes.AC_ENVIAR_ORC, "Orçamento - Enviar", SelecaoPerfil(idPerfil, Constantes.AC_ENVIAR_ORC)));
            lista.Add(ObterPermissao(Constantes.AC_COPIAR_ORC, "Orçamento - Copiar", SelecaoPerfil(idPerfil, Constantes.AC_COPIAR_ORC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_PRD, "Produtos - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_PRD)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_PRD, "Produtos - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_PRD)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_OSI, "O.S.I. e Controle de O.S.I - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_OSI)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_OSI, "O.S.I. e Controle de O.S.I - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_OSI)));

            lista.Add(ObterPermissao(Constantes.AC_CANCEL_CAD_OSI, "Controle de O.S.I. - Cancelar O.S.I.", SelecaoPerfil(idPerfil, Constantes.AC_CANCEL_CAD_OSI)));
            lista.Add(ObterPermissao(Constantes.AC_VALOR_CAD_OSI, "Controle de O.S.I. - Visualizar valor O.S.I", SelecaoPerfil(idPerfil, Constantes.AC_VALOR_CAD_OSI)));
            lista.Add(ObterPermissao(Constantes.AC_LISTA_COMP_CLI, "Lista Comparativa de Clientes", SelecaoPerfil(idPerfil, Constantes.AC_LISTA_COMP_CLI)));
            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_OSI_C, "Controle de O.S.I. Canceladas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_OSI_C)));

            return lista;
        }
        public List<Permissoes> ObterLstPermAbaTabRel(int idPerfil)
        {
            var lista = new List<Permissoes>();

            lista.Add(ObterPermissao(Constantes.AC_REL_MOV_CC, "Movimentação do caixa", SelecaoPerfil(idPerfil, Constantes.AC_REL_MOV_CC)));
            lista.Add(ObterPermissao(Constantes.AC_REL_ACOMP_DESP, "Acompanhamento anual de despesas", SelecaoPerfil(idPerfil, Constantes.AC_REL_ACOMP_DESP)));
            lista.Add(ObterPermissao(Constantes.AC_REL_EXP_CONCL, "Expedições concluídas", SelecaoPerfil(idPerfil, Constantes.AC_REL_EXP_CONCL)));
            lista.Add(ObterPermissao(Constantes.AC_REL_OSE_ABERTO, "OSES em aberto", SelecaoPerfil(idPerfil, Constantes.AC_REL_OSE_ABERTO)));


            return lista;
        }

        public List<Permissoes> ObterLstPermAbaTabAux(int idPerfil)
        {
            var lista = new List<Permissoes>();
            lista.Add(ObterPermissao(Constantes.AC_USR_CONS, "Cadastro de Usuários - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_USR_CONS)));
            lista.Add(ObterPermissao(Constantes.AC_USR_CAD, "Cadastro de Usuários - Editar", SelecaoPerfil(idPerfil, Constantes.AC_USR_CAD)));

            lista.Add(ObterPermissao(Constantes.AC_PERFIL_CONS, "Cadastro de Perfis - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_PERFIL_CONS)));
            lista.Add(ObterPermissao(Constantes.AC_PERFIL_CAD, "Cadastro de Perfis - Editar", SelecaoPerfil(idPerfil, Constantes.AC_PERFIL_CAD)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CONFIG, "Configurações para o Orçamento - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CONFIG)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CONFIG, "Configurações para o Orçamento - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CONFIG)));
            lista.Add(ObterPermissao(Constantes.AC_CONS_TABAUX, "Custos para o Orçamento - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_TABAUX)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_TABAUX, "Custos para o Orçamento - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_TABAUX)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_BANCOS, "Bancos - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_BANCOS)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_BANCO, "Bancos - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_BANCO)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_CORES, "Cores - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_CORES)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_CORES, "Cores - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_CORES)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_FACAS, "Facas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_FACAS)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_FACAS, "Facas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_FACAS)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_FORMAPGM, "Formas de Pagamentos - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_FORMAPGM)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_FORMAPGM, "Formas de Pagamentos - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_FORMAPGM)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_FORNECEDOR, "Fornecedores - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_FORNECEDOR)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_FORNECEDOR, "Fornecedores - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_FORNECEDOR)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_ITENSDESP, "Itens de Despesas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_ITENSDESP)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_ITENSDESP, "Itens de Despesas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_ITENSDESP)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_SETORES, "Setores - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_SETORES)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_SETORES, "Setores - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_SETORES)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_TIPOREC, "Tipos de recebimentos - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_TIPOREC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_TIPOREC, "Tipos de recebimentos - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_TIPOREC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_TIPODOC, "Tipos de documentos - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_TIPODOC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_TIPODOC, "Tipos de documentos - Incluir", SelecaoPerfil(idPerfil, Constantes.AC_INC_TIPODOC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_TIPODOC, "Tipos de documentos - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_TIPODOC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_TIPODOC, "Tipos de documentos - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_TIPODOC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_TPTINT, "Tipos de tinta - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_TPTINT)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_TPTINT, "Tipos de tinta - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_TPTINT)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_ZONA, "Zonas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_ZONA)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_ZONA, "Zonas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_ZONA)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_IMPRESS, "Impressores - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_IMPRESS)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_IMPRESS, "Impressores - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_IMPRESS)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_MAQ, "Máquinas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_MAQ)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_MAQ, "Máquinas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_MAQ)));


            return lista;
        }
        public List<Permissoes> ObterLstPermAbaTesouraria(int idPerfil)
        {
            var lista = new List<Permissoes>();
            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_CP, "Despesas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_CP)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_CP, "Despesas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_CP)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_CP, "Despesas - Incluir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_CP)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_CP, "Despesas - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_CP)));
            lista.Add(ObterPermissao(Constantes.AC_JUR_DESC_CAD_CP, "Despesas - Conceder acréscimo e desconto", SelecaoPerfil(idPerfil, Constantes.AC_JUR_DESC_CAD_CP)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_CR, "Receitas - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_CR)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_CR, "Receitas - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_CR)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_CR, "Receitas - Incluir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_CR)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_CR, "Receitas - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_CR)));
            lista.Add(ObterPermissao(Constantes.AC_DESC_TIT_CR, "Receitas - Desconto de títulos bancários", SelecaoPerfil(idPerfil, Constantes.AC_DESC_TIT_CR)));
            lista.Add(ObterPermissao(Constantes.AC_JUR_DESC_CAD_CR, "Receitas - Conceder acréscimo e desconto", SelecaoPerfil(idPerfil, Constantes.AC_JUR_DESC_CAD_CR)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_CC, "Contas Correntes - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_CC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_CC, "Contas Correntes - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_CC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_CC, "Contas Correntes - Incluir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_CC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_CC, "Contas Correntes - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_CC)));
            lista.Add(ObterPermissao(Constantes.AC_TRANSF_CC, "Contas Correntes - Efetuar transferências", SelecaoPerfil(idPerfil, Constantes.AC_TRANSF_CC)));

            lista.Add(ObterPermissao(Constantes.AC_FAZER_QUIT, "Fluxo de Caixa - Realizar quitação", SelecaoPerfil(idPerfil, Constantes.AC_FAZER_QUIT)));
            lista.Add(ObterPermissao(Constantes.AC_DESFAZ_QUIT, "Realizado - Desfazer quitação", SelecaoPerfil(idPerfil, Constantes.AC_DESFAZ_QUIT)));

            lista.Add(ObterPermissao(Constantes.AC_ACERT_SALDO_CC, "Acertar saldo das contas correntes", SelecaoPerfil(idPerfil, Constantes.AC_ACERT_SALDO_CC)));


            return lista;
        }

        public List<Permissoes> ObterLstPermAbaCadastro(int idPerfil)
        {
            var lista = new List<Permissoes>();
            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_CLI, "Clientes - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_CLI)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_CLI, "Clientes - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_CLI)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_CLI, "Clientes - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_CLI)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_CLI, "Clientes - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_CLI)));
            lista.Add(ObterPermissao(Constantes.AC_UNIFICAR_CAD_CLI, "Clientes - Unificar Cadastro", SelecaoPerfil(idPerfil, Constantes.AC_UNIFICAR_CAD_CLI)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_FORNEC, "Fornecedores - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_FORNEC)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_FORNEC, "Fornecedores - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_FORNEC)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_FORNEC, "Fornecedores - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_FORNEC)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_FORNEC, "Fornecedores - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_FORNEC)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_TRANS, "Transportes - Consulta", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_TRANS)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_TRANS, "Transportes - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_TRANS)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_TRANS, "Transportes - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_TRANS)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_TRANS, "Transportes - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_TRANS)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_MAT, "Matéria prima - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_MAT)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_MAT, "Matéria prima - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_MAT)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_MAT, "Matéria prima - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_MAT)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_MAT, "Matéria prima - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_MAT)));

            lista.Add(ObterPermissao(Constantes.AC_CONS_CAD_SERV, "Serviços - Consultar", SelecaoPerfil(idPerfil, Constantes.AC_CONS_CAD_SERV)));
            lista.Add(ObterPermissao(Constantes.AC_EDIT_CAD_SERV, "Serviços - Editar", SelecaoPerfil(idPerfil, Constantes.AC_EDIT_CAD_SERV)));
            lista.Add(ObterPermissao(Constantes.AC_INC_CAD_SERV, "Serviços - Inserir", SelecaoPerfil(idPerfil, Constantes.AC_INC_CAD_SERV)));
            lista.Add(ObterPermissao(Constantes.AC_APG_CAD_SERV, "Serviços - Apagar", SelecaoPerfil(idPerfil, Constantes.AC_APG_CAD_SERV)));

            return lista;
        }

        private Permissoes ObterPermissao(int id, string descrPerm, bool selecionado)
        {
            return new Permissoes()
            {
                Id = id,
                Descr = descrPerm,
                Selecionado = selecionado
            };
        }
    }
}