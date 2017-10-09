﻿using NSpec;
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
                tree.get(5).ShouldBeEquivalentTo(5);
                tree.insert(5, 6);
                tree.get(5).ShouldBeEquivalentTo(6);
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
            it["should return the value associated with the key"] = () =>
            {
                tree.insert(5, 7);
                tree.get(5).ShouldBeEquivalentTo(7);
              
            };
        };
        context["if the key is not present in the tree"] = () =>
        {
            it["should return default"] = () =>
            {
                tree.get(0).ShouldBeEquivalentTo(null);
                tree.insert(0, 0);
                tree.get(0).ShouldBeEquivalentTo(0);
            };
        };
    }
}