namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class AtendimentoPaginacaoReferenciaViewModel
    {
        public IEnumerable<AtendimentoViewModel> Atendimento { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Atendimento?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Atendimento?referencia={NextRef}&tamanho={PageSize}" : "";
    }
}
