# Conventions
 - [Globals](#Globals)
   - [Serialized](#Serialized)
     - [Privates](#Serialized-privates)
   - [Nonserialized](#Nonserialized)
     - [Privates](#Nonserialized-privates)
     - [Publics](#Publics)
 - [Locals](#Locals)
 - [Methods](#Methods)
  

# Globals
## Serialized

### Serialized privates
```cs
[SerializeField] private float conventionVariable;
```

## Nonserialized
### Nonserialized privates
```cs
// no mod
private float _conventionVariable;

// readonly 
private readonly float conventionVariable;

// static readonly
private static readonly float ConventionVariable;

// const
private const float ConventionVariable; 
```

### Publics
```cs
// no mod
public float ConventionVariable;

// readonly
public readonly float ConventionVariable;

// static readonly
public static readonly float ConventionVariable;

// const
public const float ConventionVariable;
```

# Locals
```cs
var conventionVariable;
```

# Methods
```cs
// public
public void ConventionVariable() { }

// private
private void ConventionVariable() { }
```