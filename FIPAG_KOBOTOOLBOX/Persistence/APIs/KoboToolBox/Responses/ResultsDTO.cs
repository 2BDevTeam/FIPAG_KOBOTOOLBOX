using Newtonsoft.Json;
using System.Text.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class ResultsDTO
    {
        public string start { get; set; }
        public string end { get; set; }
        public int _id { get; set; }
        public int _idd { get; set; }
        public string _uuid { get; set; }

        [JsonProperty(PropertyName = "grupo1/cidade")]
        public string cidade { get; set; }

        [JsonProperty(PropertyName = "grupo1/nome_chefe_af")]
        public string nome_chefe_af { get; set; }

        [JsonProperty(PropertyName = "grupo1/Data_de_nascimento")]
        public DateTime DataDeNascimento { get; set; }

        [JsonProperty(PropertyName = "grupo1/Pa_s_de_origem")]
        public string PaisOrigem { get; set; }

        [JsonProperty(PropertyName = "grupo1/Tem_bilhete_de_identidade_ced")]
        public string TipoDocumento { get; set; }

        [JsonProperty(PropertyName = "grupo1/N_do_Bilhete_de_Identidade")]
        public string NrBi { get; set; }

        [JsonProperty(PropertyName = "grupo1/Data_emissao_BI")]
        public DateTime DataEmissaoBI { get; set; }

        [JsonProperty(PropertyName = "grupo1/Local_de_emiss_o_do_ilhete_de_Identidade")]
        public string LocalEmissaoBI { get; set; }

        [JsonProperty(PropertyName = "grupo1/Qual_o_numero_da_c_dula_pessoal")]
        public string NrDocumento { get; set; }

        [JsonProperty(PropertyName = "grupo1/Telefone")]
        public string telefone { get; set; }

        [JsonProperty(PropertyName = "grupo1/NUIT")]
        public string nuit { get; set; }

        [JsonProperty(PropertyName = "grupo1/n_casa")]
        public int ncasa { get; set; }

        [JsonProperty(PropertyName = "grupo1/Bairro")]
        public string Bairro { get; set; }

        [JsonProperty(PropertyName = "grupo1/endereco")]
        public string endereco { get; set; }

        [JsonProperty(PropertyName = "grupo1/quarteirao")]
        public string Quarteirao { get; set; }

        [JsonProperty(PropertyName = "grupo1/estado_civil")]
        public string EstadoCivil { get; set; }

        [JsonProperty(PropertyName = "grupo1/n_membros_af")]
        public decimal NrMembrosAgregados { get; set; }

        [JsonProperty(PropertyName = "grupo3/Onde_busca_a_agua_actualmente")]
        public string FonteAguaAtual { get; set; }

        [JsonProperty(PropertyName = "grupo3/gastos_agua")]
        public decimal GastosAgua { get; set; }

        [JsonProperty(PropertyName = "grupo3/Gostaria_de_ser_beneficiario_d")]
        public string DesejaSerBeneficiario { get; set; }

        [JsonProperty(PropertyName = "grupo3/Tem_condicoes_para_p_o_de_ligacao_de_agua")]
        public string TemCondicoesLigacaoAgua { get; set; }

        [JsonProperty(PropertyName = "grupo3/Sente_que_tem_condic_stabelecida_a_liga_o")]
        public string SenteCondicoesEstabelecidasLigacaoAgua { get; set; }

        [JsonProperty(PropertyName = "grupo3/Qual_o_meio_de_pag_usar_para_pagar_gua")]
        public string MeioPagamentoAgua { get; set; }

        [JsonProperty(PropertyName = "group4/adicionado_PHC")]
        public string adicionadoPHC { get; set; }

        [JsonProperty(PropertyName = "group4/localizacao")]
        public string localizacao { get; set; }

        [JsonProperty(PropertyName = "group4/Nome_do_inquiridor")]
        public string nomeinquiridor { get; set; }

        public ValidationStatus _validation_status { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }



    public partial class ValidationStatus
    {
        public string uid { get; set; }
        public string by_whom { get; set; }
        public string label { get; set; }
    }
}
