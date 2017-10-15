using NSpec;
using FluentAssertions;
using SoftwareEngineeringBST;

class BinaryTreeTest : nspec
{
    BinaryTree<int, int?> tree;

    void before_each()
    {
        tree = new BinaryTree<int, int?>();
    }

    void describe_insert()
    {
        context["If the key is not present in the tree"] = () =>
        {
            it["should insert the value at the root position if the tree is empty"] = () =>
            {
                tree.contains(5).ShouldBeEquivalentTo(false);
                tree.insert(5, 5);
                tree.contains(5).ShouldBeEquivalentTo(true);

            };
            it["should insert the key/value pair at the correct location if the tree is not empty"] = () =>
            {
                tree.contains(4).ShouldBeEquivalentTo(false);
                tree.insert(4, 4);
                tree.contains(4).ShouldBeEquivalentTo(true);
            };
        };
        context["The key is present in the tree"] = () =>
        {
            it["should update the value of the key/value pair"] = () =>
            {
                int? result;
                tree.get(5, out result);
                result.ShouldBeEquivalentTo(5);
                tree.insert(5, 6);
                tree.get(5, out result);
                result.ShouldBeEquivalentTo(6);
            };
            it["should delete the key/value pair if the value is null"] = () =>
            {
                tree.insert(5, 5);
                tree.contains(5).ShouldBeEquivalentTo(true);
                tree.insert(5, null);
                tree.contains(5).ShouldBeEquivalentTo(false);
            };
        };
    }

    void describe_get()
    {
        context["if the key is present in the tree"] = () =>
        {
            it["should return true and return the value associated with the key in the out parameter"] = () =>
            {
                tree.insert(5, 7);
                int? result;
                tree.get(5, out result).ShouldBeEquivalentTo(true);
                result.ShouldBeEquivalentTo(7);
            };
        };
        context["if the key is not present in the tree"] = () =>
        {
            it["should return default"] = () =>
            {
                int? result;
                tree.get(0, out result).ShouldBeEquivalentTo(false);
                result.ShouldBeEquivalentTo(null);
            };
        };
    }

    void describe_contains()
    {
        it["should return true if the value is present in the tree"] = () =>
        {
            tree.contains(8).ShouldBeEquivalentTo(false);
            tree.insert(8, 8);
            tree.contains(8).ShouldBeEquivalentTo(true);
        };
        it["should return false if the value is not present in the tree"] = () =>
        {
            tree.insert(8, 8);
            tree.contains(8).ShouldBeEquivalentTo(true);
            tree = new BinaryTree<int, int?>();
            tree.contains(8).ShouldBeEquivalentTo(false);
        };
    }

    void describe_delete()
    {
        context["If the value is not present in the tree"] = () =>
        {
            it["should return false if the key is not present in the delete function"] = () =>
            {
                tree.delete(8).ShouldBeEquivalentTo(false);
            };
        };

        context["If the value is present in the tree"] = () =>
        {
            it["should remove the node with the associated key from the tree"] = () =>
            {
                tree.insert(8, 5);
                tree.delete(8).ShouldBeEquivalentTo(true);
            };
        };

    }

    void describe_toString()
    {
        context["When an empty tree is supplied"] = () =>
        {
            it["should return '()' as a string"] = () =>
            {
                tree.ToString().ShouldBeEquivalentTo("()");
            };
        };
        context["When a constructed tree is provided"] = () =>
        {
            it["should return the constructed tree as an inorder string"] = () =>
            {
                tree.insert(7, 7);   //        _7_
                tree.insert(8, 8);   //      /     \
                tree.insert(3, 3);   //    _3_      8
                tree.insert(1, 1);   //  /     \
                tree.insert(2, 2);   // 1       6
                tree.insert(6, 6);   //  \     /
                tree.insert(4, 4);   //   2   4
                tree.insert(5, 5);   //        \
                                     //         5
                tree.ToString().ShouldBeEquivalentTo("(((()1(()2()))3((()4(()5()))6()))7(()8()))");
            };
        };
    }

    void describe_lowestCommonAncestor()
    {
        context["When the tree is empty"] = () =>
        {
            it["should return false and out default(key)"] = () =>
            {
                int result;
                tree.lowestCommonAncestor(5, 7, out result).ShouldBeEquivalentTo(false);
                result.ShouldBeEquivalentTo(default(int));
            };
        };

        context["When one parameter is not present as a key in the tree"] = () =>
        {
            it["should return false and out default(key)"] = () =>
            {
                tree.insert(7, 7);
                tree.insert(5, 5);
                int result;
                tree.lowestCommonAncestor(5, 8, out result).ShouldBeEquivalentTo(false);
                result.ShouldBeEquivalentTo(default(int));
            };
        };

        context["When both parameters are present in the tree"] = () =>
        {
            it["should return the lowest common ancestor of both parameters"] = () =>
            {
                tree.insert(7, 7);
                tree.insert(5, 5);
                tree.insert(8, 8);
                int result;
                tree.lowestCommonAncestor(5, 8, out result).ShouldBeEquivalentTo(true);
                result.ShouldBeEquivalentTo(7);
            };
        };
    }
}