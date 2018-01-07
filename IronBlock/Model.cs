﻿using System;
using System.Collections.Generic;

namespace IronBlock
{
    public interface IFragment
    { 
        // probably need a method like this here:
        object Evaluate(Context context);
    }

    public class Workspace : IFragment
    {
        public Workspace()
        {
            this.Blocks = new List<IBlock>();
        }

        public IList<IBlock> Blocks {get;set;}

        public virtual object Evaluate(Context context)
        {   
            // TODO: variables
            object returnValue = null;

            foreach (var block in this.Blocks)
            {
                returnValue = block.Evaluate(context);
            }

            return returnValue;
        }
    }



    public abstract class IBlock : IFragment
    { 
        public IBlock()
        {
            this.Fields = new List<Field>();
            this.Values = new List<Value>();
            this.Statements = new List<Statement>();
            this.Mutations = new List<Mutation>();
        }

        public string Id { get; set; }
        public IList<Field> Fields { get; set; }
        public IList<Value> Values { get; set; }
        public IList<Statement> Statements { get; set; }
        public string Type { get; set; }
        public bool Inline { get; set; }
        public IBlock Next { get; set; }
        public IList<Mutation> Mutations { get; set; }
        public virtual object Evaluate(Context context)
        {
            if (null != this.Next)
            {
                return this.Next.Evaluate(context);                
            }
            return null;
        }
    
    }

    public class Statement : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(Context context)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(context);
        }
    }

    public class Value : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(Context context)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(context);
        }

    }

    public class Field
    { 
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public class Context
    {
        public Context()
        {
            this.Variables = new Dictionary<string,object>();
            this.Functions = new Dictionary<string,IFragment>();
        }
        public IDictionary<string, object> Variables { get; set; }

        public IDictionary<string, IFragment> Functions { get; set; }
    }

    public class Mutation
    {
        public Mutation(string domain, string name, string value)
        {
            this.Domain = domain;
            this.Name = name;
            this.Value = value;
        }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }


}
