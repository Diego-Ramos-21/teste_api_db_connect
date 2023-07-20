using Genesis.Integracoes.Integracao_DB.Model.Request.RqRecebeAtendimento;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaAmostra;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsRecebeAtendimento_EnviaAmostra;
using Microsoft.AspNetCore.Mvc;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqConsultaStatusAtendimento;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsConsultaStatusAtendimento;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqListaProcedimentosPendentes;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsListaProcedimentosPendentes;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaAmostrasProcedimentosPendentes;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaAmostrasProcedimentosPendentes;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaLaudoAtendimento;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaLaudoAtendimentoLista;
using Genesis.Integracoes.Integracao_DB.Model.Request.RqEnviaBase64;
using Genesis.Integracoes.Integracao_DB.Model.Response.RsEnviaBase64;
using Genesis.Integracoes.Integracao_DB.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Integracoes.Integracao_DB.Controllers
{
    [ApiController]
    public class DBClientController : ControllerBase
    {
        #region Auxiliares
        private static void logFunction(string log, int typeLog)
        {
            var logger = LogHandler.GetLogger();
            switch (typeLog)
            {
                case 1:
                    logger.Information(log);
                    break;
                case 2:
                    logger.Warning(log);
                    break;
                case 3:
                    logger.Error(log);
                    break;
                case 4:
                    logger.Fatal(log);
                    break;
                case 5:
                    logger.Verbose(log);
                    break;
                default:
                    throw new IndexOutOfRangeException($"typeLog - {typeLog}; Encontra-se fora de range adicione um type log de 1 à 5");
            }
        }
        #endregion
        #region Rotinas
        private static RecebeAtendimentoResult ResponseRecebeAtendimentoEEnviaAmostra(ct_RecebeAtendimentoEtiquetaResponse response)
        {
            try
            {
                List<StatusLote> statusLote = new List<StatusLote>();
                if (response.StatusLote != null)
                {
                    foreach (ct_StatusLote_v2 st in response.StatusLote)
                    {
                        List<StatusLotePedido> statusLotePedidos = new List<StatusLotePedido>();
                        if (st.Pedidos != null)
                        {
                            foreach (ct_StatusLotePedido_v2 p in st.Pedidos)
                            {
                                List<StatusLoteProcedimento> statusLoteProcedimentos = new List<StatusLoteProcedimento>();
                                if (p.Procedimentos != null)
                                {
                                    foreach (ct_StatusLoteProcedimento_v2 proc in p.Procedimentos)
                                    {
                                        statusLoteProcedimentos.Add(new StatusLoteProcedimento()
                                        {
                                            Material = proc.Material,
                                            DescricaoExame = proc.DescricaoExame,
                                            IdentificacaoExameApoiado = proc.IdentificacaoExameApoiado,
                                            CodigoExameDB = proc.CodigoExameDB
                                        });
                                    }
                                }

                                List<ErroIntegracao> errosPedido = new List<ErroIntegracao>();
                                if (p.ErroPedido != null)
                                {
                                    foreach (ct_ErroIntegracao_v2 err in p.ErroPedido)
                                    {
                                        errosPedido.Add(new ErroIntegracao()
                                        {
                                            Codigo = err.Codigo,
                                            Descricao = err.Descricao
                                        });
                                    }
                                }

                                List<ConfirmacaoProcedimento> confirmacaoProcedimentos = new List<ConfirmacaoProcedimento>();
                                if (p.ErrosProcedimentos != null)
                                {
                                    foreach (ct_ConfirmacaoProcedimento_v2 con in p.ErrosProcedimentos)
                                    {
                                        List<ErroIntegracao> errosProcedimento = new List<ErroIntegracao>();
                                        if (con.ErroIntegracao != null)
                                        {
                                            foreach (ct_ErroIntegracao_v2 err in con.ErroIntegracao)
                                            {
                                                errosProcedimento.Add(new ErroIntegracao()
                                                {
                                                    Codigo = err.Codigo,
                                                    Descricao = err.Descricao
                                                });
                                            }
                                        }

                                        confirmacaoProcedimentos.Add(new ConfirmacaoProcedimento()
                                        {
                                            Status = con.Status.ToString(),
                                            ErroPedido = errosProcedimento
                                        });
                                    }
                                }

                                statusLotePedidos.Add(new StatusLotePedido()
                                {
                                    NomePaciente = p.NomePaciente,
                                    PostoColeta = p.PostoColeta,
                                    NumeroAtendimentoApoiado = p.NumeroAtendimentoApoiado,
                                    NumeroAtendimentoDB = p.NumeroAtendimentoDB,
                                    Procedimentos = statusLoteProcedimentos,
                                    ErroPedido = errosPedido,
                                    ErroProcedimento = confirmacaoProcedimentos
                                });
                            }
                            statusLote.Add(new StatusLote()
                            {
                                NumeroLote = st.NumeroLote,
                                ArquivoSolicitacaoPedidos = st.ArquivoSolicitacaoPedidos,
                                DataHoraGravacao = st.DataHoraGravacao,
                                Pedidos = statusLotePedidos
                            });
                        }
                    }
                }

                List<ConfirmacaoPedido> confirmacoesPedido = new List<ConfirmacaoPedido>();
                if (response.Confirmacao.ConfirmacaoPedidov2 != null)
                {
                    foreach (ct_ConfirmacaoPedidoEtiqueta_v2 conf in response.Confirmacao.ConfirmacaoPedidov2)
                    {
                        List<AmostraEtiqueta> amostras = new List<AmostraEtiqueta>();
                        if (conf.Amostras != null)
                        {
                            foreach (ct_AmostraEtiqueta_v2 e in conf.Amostras)
                            {
                                amostras.Add(new AmostraEtiqueta()
                                {
                                    Exames = e.Exames,
                                    Material = e.Material,
                                    Origem = e.Origem,
                                    Prioridade = e.Prioridade,
                                    Volume = e.Volume,
                                    CodigoInstrumento = e.CodigoInstrumento,
                                    ContadorAmostra = e.ContadorAmostra,
                                    DataSistema = e.DataSistema,
                                    EtiquetaAmostra = e.EtiquetaAmostra,
                                    GrupoInterface = e.GrupoInterface,
                                    MeioColeta = e.MeioColeta,
                                    NomePaciente = e.NomePaciente,
                                    NumeroAmostra = e.NumeroAmostra,
                                    RegiaoColeta = e.RegiaoColeta,
                                    FlagAmostraMae = e.FlagAmostraMae,
                                    TextoAmostraMae = e.TextoAmostraMae,
                                    TipoCodigoBarras = e.TipoCodigoBarras,
                                    RGPacienteDB = e.RGPacienteDB
                                });
                            }
                        }

                        List<ConfirmacaoProcedimento> confirmacaoProcedimentos = new List<ConfirmacaoProcedimento>();
                        if (conf.Procedimentos != null)
                        {
                            foreach (ct_ConfirmacaoProcedimento_v2 c in conf.Procedimentos)
                            {
                                List<ErroIntegracao> erros = new List<ErroIntegracao>();
                                if (c.ErroIntegracao != null)
                                {
                                    foreach (ct_ErroIntegracao_v2 err in c.ErroIntegracao)
                                    {
                                        erros.Add(new ErroIntegracao()
                                        {
                                            Codigo = err.Codigo,
                                            Descricao = err.Descricao
                                        });
                                    }
                                }

                                confirmacaoProcedimentos.Add(new ConfirmacaoProcedimento()
                                {
                                    Status = c.Status.ToString(),
                                    CodigoExameDB = c.CodigoExameDB,
                                    ErroPedido = erros
                                });
                            }
                        }

                        List<ErroIntegracao> errosIntegracao = new List<ErroIntegracao>();
                        if (conf.ErroIntegracao != null)
                        {
                            foreach (ct_ErroIntegracao_v2 err in conf.ErroIntegracao)
                            {
                                errosIntegracao.Add(new ErroIntegracao()
                                {
                                    Codigo = err.Codigo,
                                    Descricao = err.Descricao
                                });
                            }
                        }

                        confirmacoesPedido.Add(new ConfirmacaoPedido()
                        {
                            Status = conf.Status.ToString(),
                            NumeroAtendimentoApoiado = conf.NumeroAtendimentoApoiado,
                            NumeroAtendimentoDB = conf.NumeroAtendimentoDB,
                            Amostras = amostras,
                            Procedimento = confirmacaoProcedimentos,
                            ErroIntegracao = errosIntegracao
                        });
                    }
                }
                Confirmacao confirmacao = new Confirmacao() { ConfirmacaoPedido = confirmacoesPedido };

                return new RecebeAtendimentoResult()
                {
                    StatusLote = statusLote,
                    Confirmacao = confirmacao
                };
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        private static StatusAtendimentoResult ResponseConsultaStatusAtendimento(ct_DadosStatusAtendimento response)
        {
            try
            {
                DadosStatusPedido dadosStatusPedido = new DadosStatusPedido()
                {
                    NumeroAtendimento = response.Pedido.NumeroAtendimento,
                    Observacao = response.Pedido.Observacao,
                    NumeroPedido = response.Pedido.NumeroPedido,
                    RegistroExterno = response.Pedido.RegistroExterno,
                    Registro = response.Pedido.Registro
                };
                List<DadosStatusProcedimento> listaDadosStatusProcedimentos = new List<DadosStatusProcedimento>();
                if (response.ListaProcedimento != null)
                {
                    foreach (ct_DadosStatusProcedimento_v1 dados in response.ListaProcedimento)
                    {
                        listaDadosStatusProcedimentos.Add(new DadosStatusProcedimento()
                        {
                            CodigoExameDB = dados.CodigoExameDB,
                            IdentificacaoExameApoiado = dados.IdentificacaoExameApoiado,
                            StatusProducao = dados.StatusProducao,
                            TipoMPP = dados.TipoMPP,
                            DataHoraRecepcaoOrigem = dados.DataHoraRecepcaoOrigem,
                            DataHoraCheckout = dados.DataHoraCheckout,
                            DataHoraRecepcaoUPF = dados.DataHoraRecepcaoUP,
                            DataHoraLiberacaoTecnica = dados.DataHoraLiberacaoTecnica,
                            DataHoraLiberacaoClinica = dados.DataHoraLiberacaoClinica,
                            DataHoraDivulgacao = dados.DataHoraDivulgacao,
                            DataHoraImpressao = dados.DataHoraImpressao
                        });
                    }
                }
                return new StatusAtendimentoResult()
                {
                    CodigoApoiado = response.CodigoApoiado,
                    CodigoSenhaIntegracao = response.CodigoSenhaIntegracao,
                    DadosStatusPedido = dadosStatusPedido,
                    DadosStatusProcedimento = listaDadosStatusProcedimentos
                };
            }
            catch (Exception e)
            {

                throw new APIException(e.Message);
            }
        }
        private static ListaProcedimentosPendentesRES ResponseListaProcedimentosPendentes(ct_ListaProcedimentosPendentesResponse_v2 response)
        {
            try
            {
                List<PedidoMPP> pedidoMPPs = new List<PedidoMPP>();
                if (response.ListaPedidos != null)
                {
                    foreach (ct_PedidoMPP_v2 dados in response.ListaPedidos)
                    {
                        List<ProcedimentoMPP> procedimentoMPPs = new List<ProcedimentoMPP>();
                        if (dados.ListaProcedimentoMPP != null)
                        {
                            foreach (ct_ProcedimentoMPP_v2 subDados1 in dados.ListaProcedimentoMPP)
                            {
                                procedimentoMPPs.Add(new ProcedimentoMPP()
                                {
                                    CodigoExameDB = subDados1.CodigoExameDB,
                                    SequenciaExameDB = subDados1.SequenciaExameDB,
                                    Status = subDados1.Status
                                });
                            }
                        }
                        pedidoMPPs.Add(new PedidoMPP() {
                            NomePaciente = dados.NomePaciente,
                            NumeroAtendimentoApoiado = dados.NumeroAtendimentoApoiado,
                            NumeroAtendimentoDB = dados.NumeroAtendimentoDB,
                            DataHoraPedido = dados.DataHoraPedido,
                            ListaProcedimentoMPP = procedimentoMPPs
                        });
                    }
                }
                return new ListaProcedimentosPendentesRES()
                {
                    ListaPedidos = pedidoMPPs
                };
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        private static AmostrasProcedimentosPendentesRES ResponseAmostrasProcedimentosPendentes(ct_EnviaAmostrasEtiquetasProcedimentosPendentesResponse_v2 response)
        {
            try
            {
                List<ListaAmostraEtiquetasPedido> amostraEtiquetasPedido = new List<ListaAmostraEtiquetasPedido>();
                if (response.PedidosAmostras != null)
                {
                    foreach (ct_ListaAmostrasEtiquetasPedido_v2 dados in response.PedidosAmostras)
                    {
                        List<AmostraEtiqueta> amostraEtiquetas = new List<AmostraEtiqueta>();
                        if (dados.Amostras != null)
                        {
                            foreach (ct_AmostraEtiqueta_v2 subDados in dados.Amostras)
                            {
                                amostraEtiquetas.Add(new AmostraEtiqueta()
                                {
                                    CodigoInstrumento = subDados.CodigoInstrumento,
                                    ContadorAmostra = subDados.ContadorAmostra,
                                    DataSistema = subDados.DataSistema,
                                    EtiquetaAmostra = subDados.EtiquetaAmostra,
                                    Exames = subDados.Exames,
                                    FlagAmostraMae = subDados.FlagAmostraMae,
                                    GrupoInterface = subDados.GrupoInterface,
                                    MeioColeta = subDados.MeioColeta,
                                    NomePaciente = subDados.NomePaciente,
                                    NumeroAmostra = subDados.NumeroAmostra,
                                    Prioridade = subDados.Prioridade,
                                    RegiaoColeta = subDados.RegiaoColeta,
                                    RGPacienteDB = subDados.RGPacienteDB,
                                    TipoCodigoBarras = subDados.TipoCodigoBarras,
                                    Material = subDados.Material,
                                    Origem = subDados.Origem,
                                    TextoAmostraMae = subDados.TextoAmostraMae,
                                    Volume = subDados.Volume
                                });
                            }
                        }
                        amostraEtiquetasPedido.Add(new ListaAmostraEtiquetasPedido()
                        {
                            NumeroAtendimentoApoiado = dados.NumeroAtendimentoApoiado,
                            NumeroAtendimentoDB = dados.NumeroAtendimentoDB,
                            AmostraEtiquetas = amostraEtiquetas
                        });
                    }
                }
                return new AmostrasProcedimentosPendentesRES() { ListaAmostraEtiquetasPedido = amostraEtiquetasPedido };
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        private static List<Result> ResponseEnviaLaudoAtendimentoLista(ct_Resultado_v2[] response)
        {
            try
            {
                List<Result> listaResults = new List<Result>();
                if (response != null)
                {
                    foreach (ct_Resultado_v2 results in response)
                    {
                        List<ResultadoProcedimento> procedimento = new List<ResultadoProcedimento>();
                        if (results.ListaResultadoProcedimentos != null)
                        {
                            foreach (ct_ResultadoProcedimentos_v2 dados in results.ListaResultadoProcedimentos)
                            {
                                List<ResultadoTexto> resultadoTextos = new List<ResultadoTexto>();
                                if (dados.ListaResultadoTexto != null)
                                {
                                    foreach (ct_ResultadoTexto_v2 subDadosTexto in dados.ListaResultadoTexto)
                                    {
                                        resultadoTextos.Add(new ResultadoTexto()
                                        {
                                            CodigoParametroDB = subDadosTexto.CodigoParametroDB,
                                            DescricaoParametroDB = subDadosTexto.DescricaoParametroDB,
                                            UnidadeMedida = subDadosTexto.UnidadeMedida,
                                            ValorReferencia = subDadosTexto.ValorReferencia,
                                            ValorResultado = subDadosTexto.ValorResultado
                                        });
                                    }
                                }
                                List<ResultadoImagem> resultadoImagem = new List<ResultadoImagem>();
                                if (dados.ListaResultadoImagem != null)
                                {
                                    foreach (ct_ResultadoImagem_v2 subDadosImagem in dados.ListaResultadoImagem)
                                    {
                                        resultadoImagem.Add(new ResultadoImagem()
                                        {
                                            CodigoParametroDB = subDadosImagem.CodigoParametroDB,
                                            ValorResultadoImagem = subDadosImagem.ValorResultadoImagem
                                        });
                                    }
                                }
                                procedimento.Add(new ResultadoProcedimento()
                                {
                                    CodigoExameDB = dados.CodigoExameDB,
                                    DataHoraLiberacaoClinica = dados.DataHoraLiberacaoClinica,
                                    DescricaoExameApoiado = dados.DescricaoExameApoiado,
                                    DescricaoMaterialApoiado = dados.DescricaoMaterialApoiado,
                                    DescricaoMetodoLogia = dados.DescricaoMetodologia,
                                    DescricaoRegiaoColeta = dados.DescricaoRegiaoColeta,
                                    IdentificacaoExameApoiado = dados.IdentificacaoExameApoiado,
                                    ListaResultadoImagem = resultadoImagem,
                                    ListaResultadoTexto = resultadoTextos,
                                    Material = dados.Material,
                                    MaterialApoiado = dados.MaterialApoiado,
                                    NomeLiberadorClinico = dados.NomeLiberadorClinico,
                                    Obervacao1 = dados.Observacao1,
                                    Obervacao2 = dados.Observacao2,
                                    Obervacao3 = dados.Observacao3,
                                    Obervacao4 = dados.Observacao4,
                                    Obervacao5 = dados.Observacao5,
                                    VersaoLaudo = dados.VersaoLaudo
                                });
                            }
                        }
                        listaResults.Add(new Result()
                        {
                            NumeroAtendimentoApoiado = results.NumeroAtendimentoApoiado,
                            NumeroAtendimentoDB = results.NumeroAtendimentoDB,
                            NomePaciente = results.NomePaciente,
                            RGPacienteApoiado = results.RGPacienteApoiado,
                            RGPacienteDB = results.RGPacienteDB,
                            Sexo = results.Sexo,
                            Peso = results.Peso,
                            Altura = results.Altura,
                            NumeroCPF = results.NumeroCPF,
                            dataNascimento = results.DataNascimento,
                            ListaResultadoProcedimento = procedimento
                        });
                    }
                }
                return listaResults;
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        private static EnviaResultadoBase64RES ResponseEnviaBase64(ct_EnviaResultadoBase64Response response)
        {
            try
            {
                return new EnviaResultadoBase64RES()
                {
                    LaudoPDF = response.LaudoPDF,
                    LinkLaudo = response.LinkLaudo,
                    Mensagem = response.Mensagem,
                    Status = response.Status
                };
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        #endregion
        
        #region Recebe Atendimento
        [HttpPost, Route("/api/v1/recebe-atendimento")]
        public async Task<RecebeAtendimentoResult> RecebeAtendimento(AtendimentoDB atendimento)
        {
            try
            {
                // Tratamento de entrad
                ct_Atendimento at = new ct_Atendimento()
                {
                    CodigoApoiado = atendimento.CodigoApoiado,
                    CodigoSenhaIntegracao = atendimento.CodigoSenhaIntegracao,
                    Pedido = new ct_Pedido_v2()
                    {
                        Altura = atendimento.Pedido.Altura.ToString(),
                        CodigoPrioridade = atendimento.Pedido.CodigoPrioridade,
                        DataHoraDUM = atendimento.Pedido.DataHoraDum.ToString("yyyy-MM-dd"),
                        DescricaoDadosClinicos = atendimento.Pedido.DescricaoDadosClinicos,
                        DescricaoMedicamentos = atendimento.Pedido.DescricaoMedicamentos,
                        NumeroAtendimentoApoiado = atendimento.Pedido.NumeroAtendimentoApoiado,
                        NumeroAtendimentoDBReserva = atendimento.Pedido.NumeroAtendimentoDBReserva,
                        Peso = atendimento.Pedido.Peso.ToString(),
                        PostoColeta = atendimento.Pedido.PostoColeta,
                        UsoApoiado = atendimento.Pedido.UsoApoiado,
                        ListaPacienteApoiado = new ct_Paciente_v2()
                        {
                            DataHoraPaciente = atendimento.Pedido.ListaPacienteApoiado.DataHoraPaciente,
                            NomePaciente = atendimento.Pedido.ListaPacienteApoiado.NomePaciente,
                            NumeroCartaoNacionalSaude = atendimento.Pedido.ListaPacienteApoiado.NumeroCartaoNacionalSaude,
                            NumeroCPF = atendimento.Pedido.ListaPacienteApoiado.NumeroCPF,
                            RGPacienteApoiado = atendimento.Pedido.ListaPacienteApoiado.RGPacienteApoiado,
                            SexoPaciente = atendimento.Pedido.ListaPacienteApoiado.SexoPaciente
                        },
                    }
                };
                List<ct_Procedimento_v2> procedimentos = new List<ct_Procedimento_v2>();
                foreach (Procedimento p in atendimento.Pedido.ListaProcedimento)
                {
                    List<ct_AmostraColeta_v2> amostras = new List<ct_AmostraColeta_v2>();
                    foreach (AmostraColeta a in p.Amostras)
                    {
                        amostras.Add(new ct_AmostraColeta_v2()
                        {
                            MeioColeta = a.MeioColeta,
                            NumeroAmostra = a.NumeroAmostra
                        });
                    }

                    procedimentos.Add(new ct_Procedimento_v2()
                    {
                        CodigoExameDB = p.CodigoExameDB,
                        DescricaoRegiaoColeta = p.DescricaoRegiaoColeta,
                        VolumeUrinario = p.VolumeUrinario,
                        MaterialApoiado = p.MaterialApoiado, 
                        DescricaoExameApoiado = p.DescricaoExameApoiado,
                        DescricaoMaterialApoiado = p.DescricaoMaterialApoiado,
                        IdentificacaoExameApoiado = p.IdentificacaoExameApoiado,
                        CodigoMPP = p.CodigoMPP,
                        Amostras = amostras.ToArray()
                    });
                }
                at.Pedido.ListaProcedimento = procedimentos.ToArray();
                List<ct_Questionario_v2> questionarios = new List<ct_Questionario_v2>();
                foreach (Questionario q in atendimento.Pedido.ListaQuestionarios)
                {
                    questionarios.Add(new ct_Questionario_v2()
                    {
                        CodigoPerguntaQuestionario = q.CodigoPerguntaQuestionario,
                        RespostaQuestionario = q.RespostaQuestionario
                    });
                }
                at.Pedido.ListaQuestionarios = questionarios.ToArray();
                // Chamada ao WS
                ct_RecebeAtendimentoEtiquetaResponse response = await new wsrvProtocoloDBSyncClient().RecebeAtendimentoAsync(at);
                // Converter objetos c# para json
                string atJson = JsonConvert.SerializeObject(at);
                string responseJson = JsonConvert.SerializeObject(response);
                // Salvamento de logs
                logFunction($"JSON ENVIADO -----> {atJson}", 1);
                logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
                // Tratamento de saída
                return ResponseRecebeAtendimentoEEnviaAmostra(response);
            }
            catch (Exception e)
            {
                throw new APIException(e.Message);
            }
        }
        #endregion
        #region Envia Amostra
        [HttpPost, Route("/api/v1/envia-amostra")]
        public async Task<RecebeAtendimentoResult> EnviaAmostra(AmostraEnvio envioAmostra)
        {
            // Tratamento de entrada
            ct_EnviaAmostrasAtendimentoRequest ea = new ct_EnviaAmostrasAtendimentoRequest()
            {
                CodigoApoiado = envioAmostra.codigoApoiado,
                CodigoSenhaIntegracao = envioAmostra.codigoSenhaIntegracao,
                NumeroAtendimentoApoiado = envioAmostra.numeroAtendimentoApoiado
            };
            // Chamada do WebService
            ct_RecebeAtendimentoEtiquetaResponse response = await new wsrvProtocoloDBSyncClient().EnviaAmostrasAsync(ea);
            // Converter objetos c# para json
            string eaJson = JsonConvert.SerializeObject(ea);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {eaJson}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseRecebeAtendimentoEEnviaAmostra(response);
        }
        #endregion
        #region Consulta Status Atendimento
        [HttpPost, Route("/api/v1/consulta-status-atendimento")]
        public async Task<StatusAtendimentoResult> ConsultaStatusAtendimento(StatusAtendimento statusAtendimento)
        {
            // Tratamento de entrada
            ct_ConsultaAtendimentoStatusRequest_v1 cas = new ct_ConsultaAtendimentoStatusRequest_v1()
            {
                CodigoApoiado = statusAtendimento.CodigoApoiado,
                CodigoSenhaIntegracao = statusAtendimento.CodigoSenhaIntegracao,
                NumeroAtendimentoApoiado = statusAtendimento.NumeroAtendimentoApoiado,
                Procedimento = statusAtendimento.Procedimento
            };
            // Chamada do WebService
            ct_DadosStatusAtendimento response = await new wsrvProtocoloDBSyncClient().ConsultaStatusAtendimentoAsync(cas);
            // Converter objetos c# para json
            string casJson = JsonConvert.SerializeObject(cas);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {casJson}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseConsultaStatusAtendimento(response);
        }
        #endregion
        #region Lista Procedimentos Pendentes
        [HttpPost, Route("/api/v1/lista-procedimentos-pendentes")]
        public async Task<ListaProcedimentosPendentesRES> ListaProcedimentosPendentes(ListaProcedimentosPendentesREQ listaProcedimentosPendentes)
        {
            // Tratamento de entrada
            ct_ListaProcedimentosPendentesRequest_v2 lpp = new ct_ListaProcedimentosPendentesRequest_v2()
            {
                CodigoApoiado = listaProcedimentosPendentes.CodigoApoiado,
                CodigoSenhaIntegracao = listaProcedimentosPendentes.CodigoSenhaIntegracao,
                dtInicial = listaProcedimentosPendentes.DtInicial,
                dtFinal = listaProcedimentosPendentes.DtFinal
            };
            // Chamada do WebService
            ct_ListaProcedimentosPendentesResponse_v2 response = await new wsrvProtocoloDBSyncClient().ListaProcedimentosPendentesAsync(lpp);
            // Converter objetos c# para json
            string lppJson = JsonConvert.SerializeObject(lpp);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {lppJson}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseListaProcedimentosPendentes(response);
        }
        #endregion
        #region Envia Amostras Procedimentos Pendentes
        [HttpPost, Route("/api/v1/envia-amostras-procedimentos-pendentes")]
        public async Task<AmostrasProcedimentosPendentesRES> EnviaAmostrasProcedimentosPendentes(AmostrasProcedimentosPendentesREQ amostrasProcedimentosPendentes)
        {
            // Tratamento de entrada
            List<ct_ProcedimentoMPP_v2> procedimentoMPPs = new List<ct_ProcedimentoMPP_v2>();
            foreach (ProcedimentoMPP subDados1 in amostrasProcedimentosPendentes.ListaPedidoMPP.ListaProcedimentoMPP)
            {
                procedimentoMPPs.Add(new ct_ProcedimentoMPP_v2()
                {
                    CodigoExameDB = subDados1.CodigoExameDB,
                    SequenciaExameDB = subDados1.SequenciaExameDB,
                    Status = subDados1.Status
                });
            }
            ct_PedidoMPP_v2 pedidoMPPs = new ct_PedidoMPP_v2()
            {
                NomePaciente = amostrasProcedimentosPendentes.ListaPedidoMPP.NomePaciente,
                NumeroAtendimentoApoiado = amostrasProcedimentosPendentes.ListaPedidoMPP.NumeroAtendimentoApoiado,
                NumeroAtendimentoDB = amostrasProcedimentosPendentes.ListaPedidoMPP.NumeroAtendimentoDB,
                DataHoraPedido = amostrasProcedimentosPendentes.ListaPedidoMPP.DataHoraPedido,
                ListaProcedimentoMPP = procedimentoMPPs.ToArray()
            };
            ct_EnviaAmostrasProcedimentosPendentesRequest_v2 eapp = new ct_EnviaAmostrasProcedimentosPendentesRequest_v2()
            {
                CodigoApoiado = amostrasProcedimentosPendentes.CodigoApoiado,
                CodigoSenhaIntegracao = amostrasProcedimentosPendentes.CodigoSenhaIntegracao,
                Amostras = pedidoMPPs
            };
            // Chamada do WebService
            ct_EnviaAmostrasEtiquetasProcedimentosPendentesResponse_v2 response = await new wsrvProtocoloDBSyncClient().EnviaAmostrasProcedimentosPendentesAsync(eapp);
            // Converter objetos c# para json
            string eappJson = JsonConvert.SerializeObject(eapp);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {eappJson}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseAmostrasProcedimentosPendentes(response);
        }
        #endregion
        #region Envia Base 64
        [HttpPost, Route("/api/v1/envia-base-64")]
        public async Task<EnviaResultadoBase64RES> EnviaBase64(EnviaResultadoBase64REQ enviaResultadoBase64REQ)
        {
            // Tratamento de entrada
            ct_EnviaResultadoBase64Request erb64 = new ct_EnviaResultadoBase64Request()
            {
                CodigoApoiado = enviaResultadoBase64REQ.CodigoApoiado,
                CodigoExameDB = enviaResultadoBase64REQ.CodigoExameDB,
                CodigoSenhaIntegracao = enviaResultadoBase64REQ.CodigoSenhaIntegracao,
                NumeroAtendimentoApoiado = enviaResultadoBase64REQ.NumeroAtendimentoApoiado,
                TipoCabecalho = enviaResultadoBase64REQ.TipoCabecalho
            };
            // Chamada do WebService
            ct_EnviaResultadoBase64Response response = await new wsrvProtocoloDBSyncClient().EnviaResultadoBase64Async(erb64);
            // Converter objetos c# para json
            string erb64Json = JsonConvert.SerializeObject(erb64);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {erb64Json}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseEnviaBase64(response);
        }
        #endregion
        #region Envia Laudo Atendimento Lista
        [HttpPost, Route("/api/v1/envia-laudo-atendimento-lista")]
        public async Task<List<Result>> EnviaLaudoAtendimentoLista(EnviaLaudoAtendimento enviaLaudoAtendimento)
        {
            // Tratamento de entrada
            List<string> numeroAtendimento = new List<string>();
            foreach (string numero in enviaLaudoAtendimento.NumeroAtendimentoApoiado) numeroAtendimento.Add(numero);
            ct_EnviaLaudoAtendimentoListaRequest_v2 elal = new ct_EnviaLaudoAtendimentoListaRequest_v2()
            {
                CodigoApoiado = enviaLaudoAtendimento.CodigoApoiado,
                CodigoSenhaIntegracao = enviaLaudoAtendimento.CodigoSenhaIntegracao,
                NumeroAtendimentoApoiado = numeroAtendimento.ToArray()
            };
            // Chamada do WebService
            ct_Resultado_v2[] response = await new wsrvProtocoloDBSyncClient().EnviaLaudoAtendimentoListaAsync(elal);
            // Converter objetos c# para json
            string elalJson = JsonConvert.SerializeObject(elal);
            string responseJson = JsonConvert.SerializeObject(response);
            // Salvamento de logs
            logFunction($"JSON ENVIADO -----> {elalJson}", 1);
            logFunction($"JSON RECEBIDO -----> {responseJson}", 1);
            // Tratamento de saída
            return ResponseEnviaLaudoAtendimentoLista(response);
        }
        #endregion
    }
}