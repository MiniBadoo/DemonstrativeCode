using System;
using System.Collections.Generic;
using System.Linq;
using FastMed.DAL.Entity.Entities.Medicines;
using FastMed.DTO.ViewModels.Filtering;
using FastMed.Infrastructure.Enums.PseudoEnums;

namespace FastMed.Filtering.Filterables.Medicines
{
    public class MedicineFilterable : BaseFilterable<Medicine>
    {
        public override IQueryable<Medicine> Filter(IQueryable<Medicine> query, List<FilteringParameter> filteringParameters)
        {
            foreach (var variable in filteringParameters)
            {
                switch (variable.Type)
                {
                    case "string":
                        query = StringGenerator(query, variable);
                        continue;
                    case "DateTime":
                        query = DateTimeGenerator(query, variable);
                        continue;
                    case "number":
                        query = NumberGenerator(query, variable);
                        continue;
                }
            }
            return query;
        }

        #region Generators

        protected override IQueryable<Medicine> DateTimeGenerator(IQueryable<Medicine> query, FilteringParameter filteringParameter)
        {
            return query;
        }

        protected override IQueryable<Medicine> NumberGenerator(IQueryable<Medicine> query, FilteringParameter filteringParameter)
        {
            return query;
        }

        protected override IQueryable<Medicine> StringGenerator(IQueryable<Medicine> query, FilteringParameter filteringParameter)
        {
            var searchText = filteringParameter.Value.ToString();
            if (string.IsNullOrEmpty(searchText))
                return query;
            searchText = searchText.ToLower();
            var searchParams = searchText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            switch (filteringParameter.ColumnName)
            {
                case nameof(Medicine.Name):
                    switch (filteringParameter.Operator)
                    {
                        case BaseOperator.Equal:
                            query = query.Where(x => x.Name.ToLower() == searchText);
                            break;
                        case BaseOperator.NotEqual:
                            query = query.Where(x => x.Name.ToLower() != searchText);
                            break;
                        case StringOperator.StartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.Name.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.EndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.Name.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.Contains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.Name.ToLower().Contains(variable));
                            }
                            break;
                        case StringOperator.NotStartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.Name.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.NotEndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.Name.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.NotContains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.Name.ToLower().Contains(variable));
                            }
                            break;
                    }
                    break;
                case nameof(Medicine.ManufacturerCompany):
                    switch (filteringParameter.Operator)
                    {
                        case BaseOperator.Equal:
                            query = query.Where(x => x.ManufacturerCompany.ToLower() == searchText);
                            break;
                        case BaseOperator.NotEqual:
                            query = query.Where(x => x.ManufacturerCompany.ToLower() != searchText);
                            break;
                        case StringOperator.StartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCompany.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.EndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCompany.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.Contains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCompany.ToLower().Contains(variable));
                            }
                            break;
                        case StringOperator.NotStartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCompany.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.NotEndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCompany.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.NotContains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCompany.ToLower().Contains(variable));
                            }
                            break;
                    }
                    break;
                case nameof(Medicine.ManufacturerCountry):
                    switch (filteringParameter.Operator)
                    {
                        case BaseOperator.Equal:
                            query = query.Where(x => x.ManufacturerCountry.ToLower() == searchText);
                            break;
                        case BaseOperator.NotEqual:
                            query = query.Where(x => x.ManufacturerCountry.ToLower() != searchText);
                            break;
                        case StringOperator.StartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCountry.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.EndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCountry.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.Contains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.ManufacturerCountry.ToLower().Contains(variable));
                            }
                            break;
                        case StringOperator.NotStartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCountry.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.NotEndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCountry.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.NotContains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.ManufacturerCountry.ToLower().Contains(variable));
                            }
                            break;
                    }
                    break;
                case nameof(Medicine.BarCode):
                    switch (filteringParameter.Operator)
                    {
                        case BaseOperator.Equal:
                            query = query.Where(x => x.BarCode.ToLower() == searchText);
                            break;
                        case BaseOperator.NotEqual:
                            query = query.Where(x => x.BarCode.ToLower() != searchText);
                            break;
                        case StringOperator.StartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.BarCode.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.EndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.BarCode.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.Contains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => x.BarCode.ToLower().Contains(variable));
                            }
                            break;
                        case StringOperator.NotStartsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.BarCode.ToLower().StartsWith(variable));
                            }
                            break;
                        case StringOperator.NotEndsWith:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.BarCode.ToLower().EndsWith(variable));
                            }
                            break;
                        case StringOperator.NotContains:
                            foreach (var variable in searchParams)
                            {
                                query = query.Where(x => !x.BarCode.ToLower().Contains(variable));
                            }
                            break;
                    }
                    break;
            }
            return query;
        }

        #endregion
    }
}