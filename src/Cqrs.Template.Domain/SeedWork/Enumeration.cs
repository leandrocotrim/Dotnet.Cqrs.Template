using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cqrs.Template.Domain.Exceptions;

namespace Cqrs.Template.Domain.SeedWork;

public abstract class Enumeration
{
    public string Name { get; }
    public int Id { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public
                                         | BindingFlags.Static | BindingFlags.DeclaredOnly);


        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        return Parse<T, int>(value, "value", item => item.Id == value);
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        return GetAll<T>().FirstOrDefault(predicate);
    }

    public static T FromName<T>(string name) where T : Enumeration
    {
        var state = GetAll<T>().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new DomainException($"Possible values for {typeof(T)}: {String.Join(",", GetAll<T>().Select(s => s.Name))}");
        }

        return state;
    }


    public static T FromId<T>(int id) where T : Enumeration
    {

        var state = GetAll<T>().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new DomainException($"Possible values for {typeof(T)}: {String.Join(",", GetAll<T>().Select(s => s.Name))}");
        }

        return state;
    }
}
