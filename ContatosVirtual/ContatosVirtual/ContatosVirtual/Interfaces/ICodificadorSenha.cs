namespace ContatosVirtual.Interfaces
{
    public interface ICodificadorSenha
    {
        string HashValue(string senhaGerada);
    }
}
