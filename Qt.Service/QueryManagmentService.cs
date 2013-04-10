using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Practices.ServiceLocation;
using Qt.Data;
using Qt.Data.Common;
using Qt.Domain.Entity;

namespace Qt.Service
{
    public class QueryManagmentService
    {
        public IEnumerable<Query> GetQueryList(IRepositoryLocator locator)
        {
            return locator.FetchAll<Query>();
        }

        public IEnumerable<Query> GetQueryListForGroup(IRepositoryLocator locator,int groupId)
        {
            var list = (IEnumerable<Query>)locator.GetById<QueryGroup>(groupId).Queries;
            return list.AsEnumerable();
        }

        public bool DeleteQuery(IRepositoryLocator locator,long id)
        {
            var q = locator.GetById<Query>(id);
            locator.Delete(q);
            return true;
        }

        public Query UpdateQuery(IRepositoryLocator locator, Query updatedQuery)
        {
            var parameters = ExtractParameters(updatedQuery);
            updatedQuery.AssociateParameters(parameters);

            return locator.Update(updatedQuery);
        }

        public Query AddQuery(IRepositoryLocator locator,Query newQuery,string userName)
        {
            //Resolve windows user name to qt user
            var chris = locator.GetById<User>(1);

            var parameters = ExtractParameters(newQuery);
            newQuery.AssociateParameters(parameters);
            
            return locator.Save(AddDefaultProperties(newQuery,chris));
        }

        public Query AddParameters(IRepositoryLocator locator, long id, List<QueryParameter> parameters )
        {
            var q = locator.GetById<Query>(id);
            q.AssociateParameters(parameters);
            return q;
        }

        private Query AddDefaultProperties(Query newQuery,User owner)
        {
            newQuery.CreatedDateTime = DateTime.Now;
            newQuery.Owner = owner;
            newQuery.Published = true;
            newQuery.DbType = EnumDatabase.Oracle;

            return newQuery;
        }

        private List<QueryParameter> ExtractParameters(Query query)
        {
            //parameters are of the form [[param_name]]
            var parameters =  new List<QueryParameter>();
            var pattern = new Regex(@"\[\[\w+\|?[\w\s]*\]\]"); //matches items like [[product_id]]
            var matches = pattern.Matches(query.Text);

            if (matches.Count == 0) return parameters;
            int paraCount = 0;
            foreach (var match in matches)
            {
                var paraArray = match.ToString().Trim(new char[]{'[',']'}).Split('|');

                var param = new QueryParameter()
                            {
                                Name = paraArray[0],
                                Description = paraArray.Length > 1 ? paraArray[1] : string.Empty,
                                ParameterNumber = paraCount++,
                            };
                parameters.Add(param);
            }

            return parameters;
        }
    }
}
