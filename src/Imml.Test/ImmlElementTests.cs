using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Moq;
using Imml.Scene;

namespace Imml.Test
{
    /// <summary>    
    /// An ImmlElement supports hierarchical nesting of other elements
    /// By default, an element has no children
    /// When nested, an elements parent is the object it is nested in    
    /// </summary>
    public class ImmlElementTests
    {
        [Fact]
        public void HasChildren_Is_False_When_No_Elements_Have_Been_Added()
        {
            var element = new ImmlElement();

            Assert.Equal(false, element.HasChildren);
        }

        [Fact]
        public void HasChildren_Is_True_When_One_Child_Has_Been_Added()
        {
            var element = new ImmlElement();

            element.Add(new ImmlElement());

            Assert.Equal(true, element.HasChildren);
        }

        [Fact]
        public void HasChildren_Should_Be_False_When_One_Child_Is_Added_Then_Removed()
        {
            var parent = new ImmlElement();
            var child = new ImmlElement();

            parent.Add(child);
            parent.Remove(child);

            Assert.Equal(false, parent.HasChildren);
        }

        [Fact]
        public void Nested_Element_Parent_Property_Should_Be_Equal_To_The_Parent_Element()
        {
            var parent = new Primitive();
            var child = new Primitive();

            parent.Add(child);

            Assert.Equal(parent, child.Parent);
        }

        [Fact]
        public void Elements_Must_Have_Globally_Unique_Names()
        {
            var name = "name1";

            var element1 = new ImmlElement { Name = name };
            var element2 = new ImmlElement { Name = name };

            var collection = new ImmlElement();
            collection.Add(element1);
            collection.Add(element2);

            Assert.NotEqual(name, element2.Name);
        }

        [Fact]
        public void ContainsName_Returns_True_When_A_Child_Element_With_That_Name_Exists()
        {
            var name = "name1";

            var element1 = new ImmlElement { Name = name };

            var collection = new ImmlElement();
            collection.Add(element1);

            Assert.True(collection.ContainsName(name));
        }

        [Fact]
        public void ContainsName_Returns_False_When_A_Child_Element_With_That_Name_Does_Not_Exist()
        {
            var name = "name1";
            var collection = new ImmlElement();

            Assert.False(collection.ContainsName(name));
        }

        [Fact]
        public void Removing_An_Element_From_An_ImmlElement_Frees_Up_That_Name_To_Be_Reused()
        {
            var name = "name1";

            var element1 = new ImmlElement { Name = name };
            var element2 = new ImmlElement { Name = name };

            var collection = new ImmlElement();
            collection.Add(element1);

            Assert.Equal(name, element1.Name);
            collection.Remove(element1);

            collection.Add(element2);
            Assert.Equal(name, element2.Name);
        }

        [Fact]
        public void Changing_An_Elements_Name_That_Exists_In_An_ImmlElement_Frees_Up_That_Name_To_Be_Reused()
        {
            var name = "name1";
            var altName = "altName";

            var element1 = new ImmlElement { Name = name };
            var element2 = new ImmlElement { Name = name };

            var collection = new ImmlElement();
            collection.Add(element1);

            Assert.Equal(name, element1.Name);
            element1.Name = altName;
            Assert.Equal(altName, element1.Name);

            collection.Add(element2);
            Assert.Equal(name, element2.Name);
        }

        [Fact]
        public void Changing_An_Elements_Name_That_Exists_In_An_ImmlElement_Means_The_Element_Is_Able_To_Be_Found_With_That_New_Name()
        {
            var name = "name1";
            var altName = "altName";

            var element = new ImmlElement { Name = name };

            var parentCollection = new ImmlElement();
            var collection = new ImmlElement();
            parentCollection.Add(collection);

            collection.Add(element);

            Assert.Equal(name, element.Name);

            //switch out the name
            element.Name = altName;

            Assert.Equal(altName, element.Name);
            Assert.False(parentCollection.ContainsName(name));
            Assert.True(parentCollection.ContainsName(altName));
        }

        [Fact]
        public void Nested_Elements_In_A_Deep_Hierarchy_Then_Added_To_A_Collection_Can_Be_Found_Via_A_Query_To_The_Collection()
        {
            var nestDepth = 10;
            var parentName = "parent";
            var childName = "child";

            var parent = new ImmlElement { Name = parentName };
            var addTo = parent;

            //nest elements 'nestDepth' deep
            for (int i = 0; i < nestDepth; i++)
            {
                var toAdd = new ImmlElement { Name = childName + i };
                addTo.Add(toAdd);

                //switch the parent to be the just added element
                addTo = toAdd;
            }

            var collection = new ImmlElement();
            collection.Add(parent);

            //at this point, collection should inherit the names of the parent collection

            for (int i = 0; i < nestDepth; i++)
            {
                Assert.True(collection.ContainsName(childName + i));
            }
        }

        [Fact]
        public void Nested_Elements_In_A_Deep_Hierarchy_Can_Be_Found_Via_A_Query_To_The_Parent_Collection()
        {
            var nestDepth = 10;
            var parentName = "parent";
            var childName = "child";

            var parent = new ImmlElement { Name = parentName };
            var addTo = parent;

            //nest elements 'nestDepth' deep
            for (int i = 0; i < nestDepth; i++)
            {
                //dynamically nest children inside the previously created element
                var toAdd = new ImmlElement { Name = childName + i };
                addTo.Add(toAdd);

                //switch the parent to be the just added element
                addTo = toAdd;
            }

            //at this point, the parent should have inherited all of the child names 
            for (int i = 0; i < nestDepth; i++)
            {
                Assert.True(parent.ContainsName(childName + i));
            }
        }

        [Fact]
        public void Removing_A_Parent_Element_Correctly_Removes_Child_Element_Names_From_The_Used_Names()
        {
            var parentName = "parent";
            var childName = "child";
            var childOfChildName = "childOfChild";

            var parent = new ImmlElement { Name = parentName };
            parent.Add(new ImmlElement { Name = childName });

            var collection = new ImmlElement();
            collection.Add(parent);

            collection.Remove(parent);
            Assert.Empty(collection.Elements);

            var child = new ImmlElement { Name = childName };
            collection.Add(child);
            Assert.Equal(childName, child.Name);

            var childOfChild = new ImmlElement { Name = childOfChildName };
            child.Add(childOfChild);

            Assert.True(collection.ContainsName(childName));
        }

        [Fact]
        public void Adding_One_Collection_As_The_Child_Of_Another_Guarantees_Names_Are_Unique()
        {
            var childName = "child1";
            var childCollectionName = "childCollection";


            var parentCollection = new ImmlElement();

            var childCollection = new ImmlElement { Name = childCollectionName };
            childCollection.Add(new ImmlElement { Name = childName });

            var childCollection2 = new ImmlElement();
            var childOfChildCollection2 = new ImmlElement { Name = childName };
            childCollection2.Add(childOfChildCollection2);

            parentCollection.Add(childCollection);
            parentCollection.Add(childCollection2);

            Assert.NotEqual(childName, childOfChildCollection2.Name);
        }

        [Fact]
        public void Dynamically_Adding_An_Element_With_Hierarchy_To_A_Collection_Then_Removing_The_Parent_Of_The_Hierarchy_Frees_The_Child_Names()
        {
            var name = "theName";

            var collection = new ImmlElement();
            collection.Name = "Context";

            var elementWithHierarchy = new ImmlElement();

            var childOfHierarchy = new ImmlElement();
            childOfHierarchy.Name = name;

            elementWithHierarchy.Add(childOfHierarchy);

            collection.Add(elementWithHierarchy);

            Assert.True(collection.ContainsName(name));

            collection.Remove(elementWithHierarchy);

            Assert.False(collection.ContainsName(name));

            collection.Add(new ImmlElement { Name = name });

            Assert.True(collection.ContainsName(name));
        }

        [Fact]
        public void TryGetElementByName_Returns_An_ImmlElement_When_Found()
        {
            var name = "PrimitiveElement";

            var primitive = new Primitive();
            primitive.Name = name;

            var collection = new ImmlElement();
            collection.Add(primitive);

            ImmlElement outElement = null;
            var found = collection.TryGetElementByName(name, out outElement);

            Assert.True(found);
            Assert.Equal(primitive, outElement);
        }

        [Fact]
        public void TryGetElementByName_Returns_Strongly_Typed_Elements_When_Found()
        {
            var name = "PrimitiveElement";

            var primitive = new Primitive();
            primitive.Name = name;

            var collection = new ImmlElement();
            collection.Add(primitive);

            Primitive outPrimitive = null;
            var found = collection.TryGetElementByName<Primitive>(name, out outPrimitive);

            Assert.True(found);
            Assert.Equal(primitive, outPrimitive);
        }

        [Fact]
        public void TryGetElementByName_Returns_False_When_An_Element_With_The_Specified_Name_Is_Not_Found()
        {
            var name = "PrimitiveElement";

            var primitive = new Primitive();
            primitive.Name = name;

            var collection = new ImmlElement();
            collection.Add(primitive);

            ImmlElement outElement = null;
            var found = collection.TryGetElementByName(Guid.NewGuid().ToString(), out outElement);

            Assert.False(found);
            Assert.Null(outElement);
        }

        [Fact]
        public void Nested_Elements_In_A_Deep_Hierarchy_Can_Be_Found_Via_A_Query_To_The_Parent_Collection_Using_TryGetElementByName()
        {
            var nestDepth = 10;
            var parentName = "parent";
            var childName = "child";

            var parent = new ImmlElement { Name = parentName };
            var addTo = parent;

            //nest elements 'nestDepth' deep
            for (int i = 0; i < nestDepth; i++)
            {
                //dynamically nest children inside the previously created element
                var toAdd = new ImmlElement { Name = childName + i };
                addTo.Add(toAdd);

                //switch the parent to be the just added element
                addTo = toAdd;
            }

            //at this point, the parent should have inherited all of the child names 
            for (int i = 0; i < nestDepth; i++)
            {
                ImmlElement outElement = null;
                Assert.True(parent.TryGetElementByName(childName + i, out outElement));
            }
        }

        [Fact]
        public void TryGetElementByID_Returns_An_ImmlElement_When_Found()
        {
            var primitive = new Primitive();

            var collection = new ImmlElement();
            collection.Add(primitive);

            ImmlElement outElement = null;
            var found = collection.TryGetElementByID(primitive.ID, out outElement);

            Assert.True(found);
            Assert.Equal(primitive, outElement);
        }

        [Fact]
        public void TryGetElementByID_Returns_Strongly_Typed_Elements_When_Found()
        {
            var primitive = new Primitive();

            var collection = new ImmlElement();
            collection.Add(primitive);

            Primitive outPrimitive = null;
            var found = collection.TryGetElementByID<Primitive>(primitive.ID, out outPrimitive);

            Assert.True(found);
            Assert.Equal(primitive, outPrimitive);
        }

        [Fact]
        public void TryGetElementByID_Returns_False_When_An_Element_With_The_Specified_ID_Is_Not_Found()
        {
            var name = "PrimitiveElement";

            var primitive = new Primitive();
            primitive.Name = name;

            var collection = new ImmlElement();
            collection.Add(primitive);

            ImmlElement outElement = null;
            var found = collection.TryGetElementByID(-1, out outElement);

            Assert.False(found);
            Assert.Null(outElement);
        }

        [Fact]
        public void Nested_Elements_In_A_Deep_Hierarchy_Can_Be_Found_Via_A_Query_To_The_Parent_Collection_Using_TryGetElementByID()
        {
            var nestDepth = 10;
            var parentName = "parent";
            var childName = "child";

            var parent = new ImmlElement { Name = parentName };
            var addTo = parent;
            var validIds = new List<int>();

            //nest elements 'nestDepth' deep
            for (int i = 0; i < nestDepth; i++)
            {
                //dynamically nest children inside the previously created element
                var toAdd = new ImmlElement { Name = childName + i };
                addTo.Add(toAdd);
                validIds.Add(toAdd.ID);

                //switch the parent to be the just added element
                addTo = toAdd;
            }

            //at this point, the parent should have inherited all of the child names 
            foreach (var id in validIds)
            {
                ImmlElement outElement = null;
                Assert.True(parent.TryGetElementByID(id, out outElement));
            }
        }

        [Fact]
        public void ImmlElement_Has_No_Defines_By_Default()
        {
            var element = new ImmlElement();
            Assert.Empty(element.GetDefines());
        }

        [Fact]
        public void Defines_Can_Be_Retrieved_When_Present()
        {
            var element = new ImmlElement();
            var define = new Define();
            define.Key = Guid.NewGuid().ToString();
            define.Value = Guid.NewGuid().ToString();

            element.Add(define);

            Assert.Equal(1, element.GetDefines().Count);
            Assert.Equal(define.Key, element.GetDefines().First().Key);
            Assert.Equal(define.Value, element.GetDefines().First().Value);
        }

        [Fact]
        public void ImmlElement_Has_No_Triggers_By_Default()
        {
            var element = new ImmlElement();
            Assert.Empty(element.GetTriggers());
        }

        [Fact]
        public void Triggers_Can_Be_Retrieved_When_Present()
        {
            var element = new ImmlElement();
            var trigger = new Trigger();
            trigger.Target = Guid.NewGuid().ToString();
            trigger.Event = EventType.DragDrop;

            element.Add(trigger);

            Assert.Equal(1, element.GetTriggers().Count);
            Assert.Equal(trigger.Target, element.GetTriggers().First().Target);
            Assert.Equal(trigger.Event, element.GetTriggers().First().Event);
        }
    }
}
