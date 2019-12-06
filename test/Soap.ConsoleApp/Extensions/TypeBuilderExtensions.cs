using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Soap.ConsoleApp.Extensions
{
    public static class TypeBuilderExtensions
    {
        public static PropertyBuilder DefineAutoProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            var fldBuilder = typeBuilder.DefineField($"_{propertyName.ToLowerInvariant()}", propertyType, FieldAttributes.Private | FieldAttributes.SpecialName);

            var getBuilder = typeBuilder.DefineMethod($"get_{propertyName}", MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.Standard, propertyType, Type.EmptyTypes);
            var getIL = getBuilder.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fldBuilder);
            getIL.Emit(OpCodes.Ret);

            var setBuilder = typeBuilder.DefineMethod($"set_{propertyName}", MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.Standard, null, new[] { propertyType });
            var setIL = setBuilder.GetILGenerator();
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fldBuilder);
            setIL.Emit(OpCodes.Ret);

            var propBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.SpecialName, propertyType, Type.EmptyTypes);
            propBuilder.SetGetMethod(getBuilder);
            propBuilder.SetSetMethod(setBuilder);
            return propBuilder;
        }
    }
}
