using AlgoDat;
using FHS.CT.AlgoDat.DataStructures;

var avlTree = new AvlTree<Person>();
avlTree.Insert(new Person("Andreas", "Bilke", new DateTime(1986, 08, 12)));
avlTree.Insert(new Person("Jakob", "Bilke", new DateTime(2013, 5, 1)));
avlTree.Insert(new Person("Lisa", "Bilke", new DateTime(1987, 3, 17)));
avlTree.Insert(new Person("Franz", "Gruber", new DateTime(2002, 2, 28)));
avlTree.Insert(new Person("Patrizia", "Huber", new DateTime(1995, 4, 3)));

var result = avlTree.Search(new Person(new DateTime(1987, 3, 17)));
Console.WriteLine($"Found {result!.Key}");