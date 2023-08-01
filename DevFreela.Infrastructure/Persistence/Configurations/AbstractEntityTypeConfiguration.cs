
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DevFreela.Infrastructure.Persistence.Configurations
{

    public abstract class AbstractEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {

        private readonly string _tableName;

        public AbstractEntityTypeConfiguration(string tableName)
        {
            _tableName = tableName;
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureTableName(builder);
            ConfigurePrimaryKey(builder);
            ConfigureColumnDefinition(builder);
            ConfigureForeingKey(builder);
        }

        private void ConfigureTableName(EntityTypeBuilder<T> builder){
            builder.ToTable(_tableName);
        }

        protected abstract void ConfigurePrimaryKey(EntityTypeBuilder<T> builder);

        protected abstract void ConfigureColumnDefinition(EntityTypeBuilder<T> builder);

        protected abstract void ConfigureForeingKey(EntityTypeBuilder<T> builder);

    }

}
