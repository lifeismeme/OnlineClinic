using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace OnlineClinic.Repositories
{
	public class CloudTableStorage : IDisposable
	{
		private CloudTable CloudTable { get; set; }

		public CloudTableStorage()
		{
			LoadTable(LoadConfig());
		}

		private IConfiguration LoadConfig()
		{
			var builder = new ConfigurationBuilder()
				//.SetBasePath(dir)
				.AddJsonFile("appsettings.json");
			return builder.Build();
		}

		private void LoadTable(IConfiguration config)
		{
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config["ConnectionStrings:AzureTableStorage:ConnectionString"]);

			var tblClient = storageAccount.CreateCloudTableClient();

			CloudTable = tblClient.GetTableReference(config["ConnectionStrings:AzureTableStorage:TableName"]);
		}

		public void Insert(TableEntity row)
		{
			var query = TableOperation.InsertOrReplace(row);
			var response = CloudTable.Execute(query);
			if (response.HttpStatusCode != 204)
				throw new Exception($"data not inserted. http response code: {response.HttpStatusCode} ");
		}

		public void Update(TableEntity row)
		{
			row.ETag = "*";
			var query = TableOperation.Replace(row);
			var response = CloudTable.Execute(query);
			if (response.HttpStatusCode != 204)
				throw new Exception($"data not replaced. http response code: {response.HttpStatusCode} ");
		}

		public T Retrieve<T>(string partitionKey, string rowKey) where T : ITableEntity
		{
			TableOperation query = TableOperation.Retrieve<T>(partitionKey, rowKey);
			TableResult response = CloudTable.ExecuteAsync(query).Result;
			if (response.Etag == null)
				throw new Exception();

			return (T)response.Result;
		}

		public IEnumerable<DynamicTableEntity> RetrieveAll() 
		{
			var query = new TableQuery();
			return CloudTable.ExecuteQuery(query);
		}

		public void Dispose()
		{
			CloudTable = null;
		}
	}
}
