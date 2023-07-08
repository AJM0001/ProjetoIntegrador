namespace MvpPesquisador.Persistencia
{
    public abstract class PersistenciaBase<TPersistencia>:ConexaoBD
    where TPersistencia : class 
    {
        private static TPersistencia instancia;
        public static TPersistencia Instancia { get { return instancia = instancia ?? Activator.CreateInstance<TPersistencia>(); } }
        
    }
}
