namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class OcorrenciaPaginacaoReferenciaViewModel
    {
        public IEnumerable<OcorrenciaViewModel> Ocorrencia { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Ocorrencia?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Ocorrencia?referencia={NextRef}&tamanho={PageSize}" : "";
    }
}
