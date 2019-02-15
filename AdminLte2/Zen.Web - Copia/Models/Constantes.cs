using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public static class Constantes
    {
        public const int TamanhoPagina = 10;

        public const int AC_USR_CONS = 10;
        public const int AC_USR_CAD = 11;
        public const int AC_PERFIL_CONS = 12;
        public const int AC_PERFIL_CAD = 13;

        // cadastros
        //  clientes

        public const int AC_CONS_CAD_CLI = 1;
        public const int AC_EDIT_CAD_CLI = 2;
        public const int AC_INC_CAD_CLI = 3;
        public const int AC_APG_CAD_CLI = 4;
        public const int AC_UNIFICAR_CAD_CLI = 5;

        //fornecedores
        public const int AC_CONS_CAD_FORNEC = 51;
        public const int AC_EDIT_CAD_FORNEC = 52;
        public const int AC_INC_CAD_FORNEC = 53;
        public const int AC_APG_CAD_FORNEC = 54;

        // matéria-prima    
        public const int AC_CONS_CAD_MAT = 61;
        public const int AC_EDIT_CAD_MAT = 62;
        public const int AC_INC_CAD_MAT = 63;
        public const int AC_APG_CAD_MAT = 64;

        // tesouraria
        //contas a pagar
        public const int AC_CONS_CAD_CP = 101;
        public const int AC_EDIT_CAD_CP = 102;
        public const int AC_INC_CAD_CP = 103;
        public const int AC_APG_CAD_CP = 104;
        public const int AC_JUR_DESC_CAD_CP = 105;

        //contas a receber
        public const int AC_CONS_CAD_CR = 111;
        public const int AC_EDIT_CAD_CR = 112;
        public const int AC_INC_CAD_CR = 113;
        public const int AC_APG_CAD_CR = 114;
        public const int AC_DESC_TIT_CR = 115;
        public const int AC_JUR_DESC_CAD_CR = 116;

        // contas correntes 
        public const int AC_CONS_CAD_CC = 121;
        public const int AC_EDIT_CAD_CC = 122;
        public const int AC_INC_CAD_CC = 123;
        public const int AC_APG_CAD_CC = 124;
        public const int AC_TRANSF_CC = 125;

        //realizado
        public const int AC_DESFAZ_QUIT = 130;
        public const int AC_FAZER_QUIT = 131;

        //acertar saldo cc
        public const int AC_ACERT_SALDO_CC = 140;

        // Servicos
        public const int AC_CONS_CAD_SERV = 151;
        public const int AC_EDIT_CAD_SERV = 152;
        public const int AC_INC_CAD_SERV = 153;
        public const int AC_APG_CAD_SERV = 154;

        // tabelas auxiliares
        // tabelas auxiliares para a tesouraria
        public const int AC_CONS_BANCOS = 201;
        public const int AC_EDIT_CAD_BANCO = 202;
        public const int AC_INC_CAD_BANCO = 203;

        public const int AC_CONS_FORMAPGM = 207;
        public const int AC_EDIT_FORMAPGM = 208;
        public const int AC_INC_FORMAPGM = 208;
        public const int AC_APG_FORMAPGM = 208;

        public const int AC_CONS_FORNECEDOR = 209;
        public const int AC_EDIT_FORNECEDOR = 210;

        public const int AC_CONS_ITENSDESP = 211;
        public const int AC_EDIT_ITENSDESP = 212;
        public const int AC_INC_ITENSDESP = 212;
        public const int AC_APG_ITENSDESP = 212;

        public const int AC_CONS_SETORES = 213;
        public const int AC_EDIT_SETORES = 214;
        public const int AC_INC_SETORES = 214;
        public const int AC_APG_SETORES = 214;

        public const int AC_CONS_TIPODOC = 215;
        public const int AC_EDIT_TIPODOC = 216;
        public const int AC_INC_TIPODOC = 216;
        public const int AC_APG_TIPODOC = 216;

        public const int AC_CONS_TIPOREC = 217;
        public const int AC_EDIT_TIPOREC = 218;
        public const int AC_INC_TIPOREC = 218;
        public const int AC_APG_TIPOREC = 218;

        //tabelas auxiliares para o orçamento
        public const int AC_CONS_CONFIG = 251;
        public const int AC_EDIT_CONFIG = 252;

        public const int AC_CONS_TABAUX = 255;
        public const int AC_EDIT_TABAUX = 256;

        public const int AC_CONS_ZONA = 257;
        public const int AC_EDIT_ZONA = 258;
        public const int AC_INC_ZONA = 258;
        public const int AC_APG_ZONA = 258;

        //tabelas auxiliares para a produção
        // impressores
        public const int AC_CONS_CAD_IMPRESS = 271;
        public const int AC_EDIT_CAD_IMPRESS = 272;
        public const int AC_INC_CAD_IMPRESS = 272;
        public const int AC_APG_CAD_IMPRESS = 272;

        // máquinas
        public const int AC_CONS_CAD_MAQ = 273;
        public const int AC_EDIT_CAD_MAQ = 274;
        public const int AC_INC_CAD_MAQ = 274;
        public const int AC_APG_CAD_MAQ = 274;

        // facas
        public const int AC_CONS_CAD_FACAS = 275;
        public const int AC_EDIT_CAD_FACAS = 276;
        public const int AC_INC_CAD_FACAS = 276;
        public const int AC_APG_CAD_FACAS = 276;

        //cores
        public const int AC_CONS_CAD_CORES = 277;
        public const int AC_EDIT_CAD_CORES = 278;
        public const int AC_INC_CAD_CORES = 278;
        public const int AC_APG_CAD_CORES = 278;
        public const int AC_UNIF_CAD_CORES = 279;

        // tipo de tinta
        public const int AC_CONS_CAD_TPTINT = 281;
        public const int AC_EDIT_CAD_TPTINT = 282;
        public const int AC_INC_CAD_TPTINT = 282;
        public const int AC_APG_CAD_TPTINT = 282;
        public const int AC_UNIF_CAD_TPTINT = 283;

        //transportes
        public const int AC_CONS_CAD_TRANS = 285;
        public const int AC_EDIT_CAD_TRANS = 286;
        public const int AC_INC_CAD_TRANS = 287;
        public const int AC_APG_CAD_TRANS = 288;

        //relatórios
        // tesouraria / cobranças
        public const int AC_REL_MOV_CC = 301;
        public const int AC_REL_ACOMP_DESP = 302;

        // produção
        public const int AC_REL_EXP_CONCL = 310;
        public const int AC_REL_OSE_ABERTO = 311;

        //pedido de orçamento
        public const int AC_CONS_CAD_PEDORC = 401;
        public const int AC_EDIT_CAD_PEDORC = 402;
        public const int AC_INC_CAD_PEDORC = 403;
        public const int AC_APG_CAD_PEDORC = 404;

        //orçamento
        public const int AC_CONS_CAD_ORC = 411;
        public const int AC_EDIT_CAD_ORC = 412;
        public const int AC_INC_CAD_ORC = 413;
        public const int AC_APG_CAD_ORC = 414;
        public const int AC_ENVIAR_ORC = 414;

        public const int AC_CONS_CAD_PRD = 416;
        public const int AC_EDIT_CAD_PRD = 417;
        public const int AC_COPIAR_ORC = 418;

        //osi
        public const int AC_CONS_CAD_OSI = 421;
        public const int AC_EDIT_CAD_OSI = 422;
        public const int AC_INC_CAD_OSI = 423;
        public const int AC_APG_CAD_OSI = 424;
        public const int AC_CANCEL_CAD_OSI = 425;
        public const int AC_VALOR_CAD_OSI = 426;
        public const int AC_LISTA_COMP_CLI = 427;

        // osi cancelada 
        public const int AC_CONS_CAD_OSI_C = 431;
        public const int AC_REATIV_CAD_OSI_C = 432;

        //Produção
        public const int AC_CONS_PRD_OSI = 501;
        public const int AC_EDIT_PRD_OSI = 502;

        public const int AC_EDIT_PRD_ART = 511;
        public const int AC_EDIT_PRD_FILM = 512;
        public const int AC_EDIT_PRD_MATR = 513;
        public const int AC_EDIT_PRD_MAT = 514;
        public const int AC_EDIT_PRD_CORT = 515;
        public const int AC_EDIT_PRD_FACA = 516;
        public const int AC_EDIT_PRD_TINT = 517;
        public const int AC_EDIT_PRD_IMP = 518;
        public const int AC_EDIT_PRD_ACAB = 520;
        public const int AC_EDIT_PRD_PREVIA = 521;
        public const int AC_EDIT_PRD_URGENTE = 522;

        public const int AC_MOVER_LOTE = 525;
        public const int AC_QUEBRAR_LOTE = 526;
        public const int AC_CONS_PRD_DIA = 527;
        public const int AC_EDIT_PRD_DIA = 528;
        public const int AC_FINA_PRD_DIA = 529;

        public const int AC_PRD_VALORES = 530;

        public const int AC_PRD_VER_ACAB = 540;
        public const int AC_PRD_EDIT_ACAB = 541;
        public const int AC_PRD_VER_IMP = 542;
        public const int AC_PRD_EDIT_IMP  = 543;
        public const int AC_PRD_VER_TINTA = 544;
        public const int AC_PRD_EDIT_TINTA = 545;
        public const int AC_PRD_VER_MATRIZ = 546;
        public const int AC_PRD_EDIT_MATRIZ = 547;
        public const int AC_EDT_SETOR_ACAB = 548;
        public const int AC_EDT_SETOR_IMP = 549;
        public const int AC_EDT_SETOR_TINTA = 550;
        public const int AC_EDT_SETOR_MATRIZ = 551;

        public const int AC_CONS_PRD_EXP = 570;
        public const int AC_EDIT_PRD_EXP = 571;
        public const int AC_INC_PRD_EXP = 572;
        public const int AC_APG_PRD_EXP = 573;
        public const int AC_EDIT_DAD_EXP = 575;
        public const int AC_EDIT_DESC_EXP = 576;
        public const int AC_MOD_CLI_EXP = 577;

        public const int AC_VIS_ENT_EXP_PEND = 578;
        public const int AC_LISTA_PRODDIARIA = 579;

        public const int AC_CONS_OSE = 580;
        public const int AC_EDIT_OSE = 581;
        public const int AC_INC_OSE = 582;
        public const int AC_APG_OSE = 583;
        public const int AC_EDIT_VALOR_OSE = 584;
        public const int AC_EDIT_VALORPREV_OSE = 585;
        public const int AC_CANCEL_OSE = 586;


    }
}