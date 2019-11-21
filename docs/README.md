# Learning C# by Programming Games, second edition

This repository contains the sample code, solutions to the exercises and game assets belonging to the book ["Learning C# by Programming Games", second edition](https://www.springer.com/gp/book/9783662592519), published by Springer.

<p align="center">
  <img width="250" alt="Cover image of the book" src="Cover.png">
</p>

## How to Get Started
Assuming you own a copy of the book, here's what you need to do to get started with game development:
1. Download and install [Visual Studio Community 2017](https://visualstudio.microsoft.com/vs/older-downloads/)*, a free program by Microsoft for writing and compiling code in C#. Follow the installation instructions, and make sure to (at least) include the components for C# development. You will see many more options (and you're free to install them), but you won't need them for this book.
2. Download and install [MonoGame 3.6](http://www.monogame.net/2017/03/01/monogame-3-6/)*, an open-source library that adds game-specific functionality to the C# language.
3. Download and install the [Visual C++ 2012 Redistributable for VS2012](https://www.microsoft.com/en-us/download/confirmation.aspx?id=30679). This is required for running any MonoGame project that has text in it. (Otherwise, you will get error pop-ups about missing files such as "freetype6.dll".)
4. Read Chapter 1 of the book (except Section 1.6) and follow the instructions there. This will let you run and change your first MonoGame project in Visual Studio, to verify that the installation worked.
5. Download all example programs from our repository. To do this, go to our GitHub repository [(github.com/egges/csharpgames)](https://github.com/egges/csharpgames), click the green 'Clone or Download' button near the top, and choose 'Download ZIP'. Download the ZIP file to a location on your PC. Then **extract (unpack) the ZIP file**, otherwise the programs won't work!
6. Read Section 1.6 of the book and follow the instructions there. This will let you open an example project and play the first game of our book: Painter.

Once you've completed these steps successfully, you're ready to enjoy the book to its fullest!

(*) Both Visual Studio and MonoGame receive new versions quite often. This is difficult for us to keep track of, especially with a printed book. We advise you to use Visual Studio 2017 and MonoGame 3.6, to be 100% sure that the example projects will run. If you really want to use newer versions, then you are free to do this at your own risk.

## About the Book
Developing computer games is a perfect way to learn how to program in modern programming languages. This book teaches you how to program in the language C# through the creation of computer games – and without requiring any previous programming experience.

Contrary to most programming books, we do not organize the presentation according to programming language constructs, but instead use the structure and elements of computer games as a framework. For instance, there are chapters on dealing with player input, game objects, game worlds, game states, levels, animation, physics, and intelligence. The reader will be guided through the development of four games showing the various aspects of game development. Starting with a simple shooting game, we move on to puzzle games consisting of multiple levels, and conclude the book by developing a full-fledged platform game with animation, game physics, and intelligent enemies. We show a number of commonly used techniques in games, such as drawing layers of sprites, rotating, scaling and animating sprites, dealing with physics, handling interaction between game objects, and creating pleasing visual effects. At the same time, we provide a thorough introduction to C# and object-oriented programming, introducing step by step important programming concepts such as loops, methods, classes, collections, and exception handling.

The book is also designed to be used as a basis for a game-oriented programming course at university level. Supplementary materials for organizing such a course (including solutions to all exercises) are available in this repository, along with all example programs, game sprites, and sounds.

## About the Second Edition
This second edition of the book includes the following improvements: 
- The book and all example programs are now based on the library MonoGame 3.6, instead of the obsolete XNA Game Studio. 
- Instead of explaining how the example programs work, the text now invites readers to write these programs themselves, with clearly marked reference points throughout the text. 
- The book now makes a clearer distinction between general (C#) programming concepts and concepts that are specific to game development. 
- The most important programming concepts are now summarized in convenient “Quick Reference” boxes, which replace the syntax diagrams of the first edition. 
- The updated exercises are now grouped per chapter and can be found at the end of each chapter, allowing readers to test their knowledge more directly.

## Legacy
In case you need the solutions to the exercises or the set of code samples of the first edition of the book, you can find them in the *legacy* folder of the GitHub repository [(github.com/egges/csharpgames)](https://github.com/egges/csharpgames).

## About the Authors
**Wouter van Toll** is a post-doctoral researcher at Inria in Rennes, France, as well as a fanatic developer of games and apps. His research focuses on simulating the behavior of human crowds. Previously, he was a lecturer at the Department of Information and Computing Sciences at Utrecht University in the Netherlands. He has taught several bachelor and master courses there, including the introductory Game programming course designed by co-author Arjan Egges.

**Arjan Egges** is a part-time lecturer in the Department of Information and Computing Sciences at Utrecht University in the Netherlands. He has ample teaching experience related to games and computer animation, and he designed the introductory programming course for the university's Game Technology bachelor program. Furthermore, Arjan is has launched a platform called [Quarterfall](https://quarterfall.com), an ICT education tool for creating assignments for students with an automatic feedback mechanism.

**Jeroen D. Fokker** is an assistant professor in the Software Technology group at Utrecht University. As the director of education, he is responsible for the undergraduate programs in Computer Science and Information Science. He has been teaching introductory programming courses for over 20 years, using C++, Haskell, Java, and C#, as well as courses on compiler construction.

## License
The contents of this repository, including all the code and the game assets, is released under the MIT license and as such is free to use for open source and commercial applications. 

The example games in this book use the following music pieces:
- Painter - Blipotron (by Kevin MacLeod)
- JewelJam - Klockworx (by Kevin MacLeod)
- PenguinPairs - Nouvelle Noel (by Kevin MacLeod)
- TickTick - Getting it Done (by Kevin MacLeod)

The music is available on [Incompetech](http://www.incompetech.com) and it is licensed under [Creative Commons: By Attribution 3.0](https://creativecommons.org/licenses/by/3.0).
