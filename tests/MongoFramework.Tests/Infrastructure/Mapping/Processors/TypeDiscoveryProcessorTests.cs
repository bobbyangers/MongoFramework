﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using MongoFramework.Attributes;
using MongoFramework.Infrastructure.Mapping;
using MongoFramework.Infrastructure.Mapping.Processors;
using MongoFramework.Infrastructure.Serialization;

namespace MongoFramework.Tests.Infrastructure.Mapping.Processors
{
	[TestClass]
	public class TypeDiscoveryProcessorTests : MappingTestBase
	{
		public class NoTypeDiscoveryAttributeModel
		{

		}

		[RuntimeTypeDiscovery]
		public class TypeDiscoveryAttributeModel
		{

		}

		[TestMethod]
		public void TypeDiscoverySerializerWhenAttributeIsDefined()
		{
			EntityMapping.AddMappingProcessor(new TypeDiscoveryProcessor());

			var serializer = BsonSerializer.LookupSerializer<TypeDiscoveryAttributeModel>();
			Assert.AreNotEqual(typeof(TypeDiscoverySerializer<>), serializer.GetType().GetGenericTypeDefinition());

			EntityMapping.RegisterType(typeof(TypeDiscoveryAttributeModel));

			serializer = BsonSerializer.LookupSerializer<TypeDiscoveryAttributeModel>();
			Assert.AreEqual(typeof(TypeDiscoverySerializer<>), serializer.GetType().GetGenericTypeDefinition());
		}

		[TestMethod]
		public void NotTypeDiscoverySerializerWhenAttributeNotDefined()
		{
			EntityMapping.AddMappingProcessor(new TypeDiscoveryProcessor());

			var serializer = BsonSerializer.LookupSerializer<NoTypeDiscoveryAttributeModel>();
			Assert.AreNotEqual(typeof(TypeDiscoverySerializer<>), serializer.GetType().GetGenericTypeDefinition());

			EntityMapping.RegisterType(typeof(NoTypeDiscoveryAttributeModel));

			serializer = BsonSerializer.LookupSerializer<NoTypeDiscoveryAttributeModel>();
			Assert.AreNotEqual(typeof(TypeDiscoverySerializer<>), serializer.GetType().GetGenericTypeDefinition());;
		}
	}
}
