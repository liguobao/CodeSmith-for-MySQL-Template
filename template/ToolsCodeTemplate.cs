using System;
using SchemaExplorer;
using System.Data;
using CodeSmith.Engine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

public class ToolsCodeTemplate:CodeTemplate
{
    public string GetColumnComment(ColumnSchema column)
    {
         return column.Description;
    }
    
    
	public string GetModelClassName(TableSchema table)
	{
		string result;
		if ( table.ExtendedProperties.Contains("ModelName") )
		{
			result = ((string)table.ExtendedProperties["ModelName"].Value).Substring(3);	
			//return MakePascal(result);        
		}	
		if (table.Name.EndsWith("s"))
		{
			result = MakeSingle(table.Name).Substring(3);
		}
		else
		{
			result = table.Name.Substring(3);
		}	
		return MakePascal(result);
       
	}
	public string GetPropertyType(ColumnSchema column)
	{
		return GetCSharpTypeFromDBFieldType(column);
	}
	public string GetCSharpTypeFromDBFieldType(ColumnSchema column)
	{
		if (column.Name.EndsWith("TypeCode")) return column.Name;
		string type;
		switch (column.DataType)
		{
			case DbType.AnsiString: type= "string";break;
			case DbType.AnsiStringFixedLength: type= "string";break;
			case DbType.Binary: type= "byte[]";break;
			case DbType.Boolean: type= "bool";break;
			case DbType.Byte: type= "byte";break;
			case DbType.Currency: type= "decimal";break;
			case DbType.Date: type= "DateTime";break;
			case DbType.DateTime: type= "DateTime";break;
			case DbType.Decimal: type= "decimal";break;
			case DbType.Double: type= "double";break;
			case DbType.Guid: type= "Guid";break;
			case DbType.Int16: type= "short";break;
			case DbType.Int32: type= "int";break;
			case DbType.Int64: type= "long";break;
			case DbType.Object: type= "object";break;
			case DbType.SByte: type= "sbyte";break;
			case DbType.Single: type= "float";break;
			case DbType.String: type= "string";break;
			case DbType.StringFixedLength: type= "string";break;
			case DbType.Time: type= "TimeSpan";break;
			case DbType.UInt16: type= "ushort";break;
			case DbType.UInt32: type= "uint";break;
			case DbType.UInt64: type= "ulong";break;
			case DbType.VarNumeric: type= "decimal";break;
			default:
			{
				type= "__UNKNOWN__" + column.NativeType;
				break;
			}
		}
		if(column.AllowDBNull&&
			column.SystemType.IsValueType)
		{
			type=type+"?";
		}
		return type;
	}
	
	public string GetPropertyName(ColumnSchema column)
	{
		//return MakePascal(GetNameFromDBFieldName(column));
        return GetNameFromDBFieldName(column);
	}
	public string GetNameFromDBFieldName(ColumnSchema column)
	{
		return column.Name;
	}
	
	//I will be dirty too! -- coded by shenbo
	public string MakePascal(string value)
	{
		return value.Substring(0, 1).ToUpper() + value.Substring(1);
	}
	
	public string MakeSingle(string name)
	{
		Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
		Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
		Regex plural3 = new Regex("(?<keep>[sxzh])es$");
		Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");
	
		if(plural1.IsMatch(name))
			return plural1.Replace(name, "${keep}y");
		else if(plural2.IsMatch(name))
			return plural2.Replace(name, "${keep}");
		else if(plural3.IsMatch(name))
			return plural3.Replace(name, "${keep}");
		else if(plural4.IsMatch(name))
			return plural4.Replace(name, "${keep}");
	
		return name;
	}
	public void PrintHeader()
	{
		Response.WriteLine("//============================================================");
		Response.WriteLine("//http://codelover.link author:李国宝");
		Response.WriteLine("//============================================================");
		Response.WriteLine();
	}
	
	public string GetPKName(TableSchema TargetTable)
	{
		if (TargetTable.PrimaryKey != null)
		{
			if (TargetTable.PrimaryKey.MemberColumns.Count == 1)
			{
				return TargetTable.PrimaryKey.MemberColumns[0].Name;
			}
			else
			{
				throw new Exception("This template will not work on primary keys with more than one member column.");
			}
		}
		else
		{
			throw new Exception("This template will only work on tables with a primary key.");
		}
	}
	
	public string GetPKType(TableSchema TargetTable)
	{
		if (TargetTable.PrimaryKey != null)
		{
			if (TargetTable.PrimaryKey.MemberColumns.Count == 1)
			{
				return GetCSharpTypeFromDBFieldType(TargetTable.PrimaryKey.MemberColumns[0]);
			}
			else
			{
				throw new ApplicationException("This template will not work on primary keys with more than one member column.");
			}
		}
		else
		{
			throw new ApplicationException("This template will only work on MyTables with a primary key.");
		}
	}
	
	public string MakeCamel(string value)
	{
		return value.Substring(0, 1).ToLower() + value.Substring(1);
	}
	
	public string MakePlural(string name)
	{
		Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
		Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
		Regex plural3 = new Regex("(?<keep>[sxzh])$");
		Regex plural4 = new Regex("(?<keep>[^sxzhy])$");
	
		if(plural1.IsMatch(name))
			return plural1.Replace(name, "${keep}ies");
		else if(plural2.IsMatch(name))
			return plural2.Replace(name, "${keep}s");
		else if(plural3.IsMatch(name))
			return plural3.Replace(name, "${keep}es");
		else if(plural4.IsMatch(name))
			return plural4.Replace(name, "${keep}s");
	
		return name;
	}
}