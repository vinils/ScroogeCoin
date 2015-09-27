# ScroogeCoin

This breanch contais the last copy of GoofyCoin project which is based for.

Improving Googy coin - Fixing double spend attack, enabling persons to slpit coins, removing coin class




To solve the double-spending problem, we’ll design another cryptocurrency, which we’ll call
ScroogeCoin. We’ll build off of GoofyCoin, but is a bit more complicated in terms of data structures.
The first key idea is that a designated entity called Scrooge publishes a history of all the transactions
that have happened. To do this he uses a block chain, that data structure we discussed before, which
is digitally signed by Scrooge. It’s a series of data blocks, each with one transaction in it (in practice, as
an optimization, we’d really put multiple transactions into the same block, as Bitcoin does.) Each block
has the ID of a transaction, the transaction’s contents, and a hash pointer to the previous block.
Scrooge digitally signs the final hash pointer, which represents this entire structure, and publishes the
signature along with the block chain.

![Alt text](https://github.com/vinils/ScroogeCoin/blob/master/Figure111.jpg "Figure 1.11")


Figure 1.11 ScroogeCoin block chain.

In ScroogeCoin a transaction only counts if it is in the block chain signed by Scrooge. Anybody can
verify that a transaction was endorsed by Scrooge by checking Scrooge’s signature on the block that it
appears in. Scrooge makes sure that he doesn’t endorse a transaction that attempts to double-spend
an already spent coin.
Why do we need a block chain with hash pointers in addition to having Scrooge sign each block? A
block chain prevents Scrooge from being able to change his mind about the history of transactions. If
he wants to add or remove a transaction to the history, or change an existing transaction, it will affect
all of the following blocks because of the hash pointers. As long as someone is monitoring the latest
hash pointer published by Scrooge, the change will be obvious and easy to catch. In a system where
Scrooge signed blocks individually, you’d have to keep track of every single signature Scrooge ever
issued. A block chain makes it very easy for any two individuals to verify that they have observed the
exact same history of transactions signed by Scrooge.
In ScroogeCoin, there are two kinds of transactions. The first kind is CreateCoins, which is just like the
operation Goofy could do in GoofyCoin that makes a new coin. With ScroogeCoin, we’ll extend the
semantics a bit to allow multiple coins to be created in one transaction.

![Alt text](https://github.com/vinils/ScroogeCoin/blob/master/Figure112.jpg "Figure 1.12")


Figure 1.12 CreateCoin transaction. This CreateCoin transaction that creates multiple coins. Each coin
has a serial number within the transaction. Each coin also has a value; it’s worth a certain number of
ScroogeCoins. Finally, each coin has a recipient, which is a public key that gets the coin when it’s
created. So CreateCoin creates a bunch of new coins with different values and assigns them to people
as initial owners. We refer to coins by CoinIDs. A CoinID is a combination of a transaction ID and the
coin’s serial number within that transaction.

A CreateCoins transaction is always valid by definition if it is signed by Scrooge. We won’t worry about
when Scrooge is entitled to create coins or how many, just like we didn’t worry in GoofyCoin about
how Goofy is chose to create coins.
The second kind of transaction is PayCoins. It consumes some coins, that is, destroys them, and
creates new coins of the same total value. The new coins might belong to different people (public
keys). This transaction has to be signed by everyone who’s paying in a coin. So if you’re the owner of
one of the coins that’s going to be consumed in this transaction, then you need to digitally sign the
transaction to say that you’re really okay with spending this coin.

![Alt text](https://github.com/vinils/ScroogeCoin/blob/master/Figure113.jpg "Figure 1.13")


Figure 1.13 A PayCoins Transaction.

The rules of ScroogeCoin say that PayCoins transaction is valid if four things are true:
? The consumed coins are valid, that is, they really were created in previous transactions.
? The consumed coins were not already consumed in some previous transaction. That is, that
this is not a double-spend.
? The total value of the coins that come out of this transaction is equal to the total value of the
coins that went in. That is, only Scrooge can create new value.
? The transaction is validly signed by the owners of all of the consumed coins.
If all of those conditions are met, then this PayCoins transaction is valid and Scrooge will accept it.
He’ll write it into the history by appending it to the block chain, after which everyone can see that this
transaction has happened. It is only at this point that the participants can accept that the transaction
has actually occurred-until it is published, it might be preempted by a douple-spending transaction
even if it is otherwise valid by the first three conditions.
Coins in this system are immutable — they are never changed, subdivided, or combined. Each coin is
created, once, in one transaction and later consumed in some other transaction. But we can get the
same effect as being able to subdivide, or pay on or combine coins, by using transactions. For
example, to subdivide a coin, Alice create a new transaction that consumes that one coin, and then
produces two new coins of the same total value. Those two new coins could be assigned back to her.
So although coins are immutable in this system, it has all the flexibility of a system that didn’t have
immutable coins.
Now, we come to the core problem with ScroogeCoin. ScroogeCoin will work in the sense that people
can see which coins are valid. It prevents double-spending, because everyone can look into the block
chain and see that all of the transactions are valid and that every coin is consumed only once. But the
problem is Scrooge — he has too much influence. He can’t create fake transactions, because he can’t
forge other people’s signatures. But he could stop endorsing transactions from some users, denying
them service and making their coins unspendable. If Scrooge is greedy (as his cartoon namesake
suggests) he could refuse to publish transactions unless they transfer some mandated transaction fee
to him. Scrooge can also of course create as many new coins for himself as he wants. Or Scrooge
could get bored of the whole system and stop updating the block chain completely.
The problem here is centralization. Although Scrooge is happy with this system, we, as users of it,
might not be. While ScroogeCoin may seem like an unrealistic proposal, much of the early research on
cryptosystems assumed there would indeed be some central trusted authority, typically referred to as
a bank. After all, most real-world currencies do have a trusted issuer (typically a government mint)
responsible for creating currency and determining which notes are valid. However, cryptocurrencies
with a central authority largely failed to take off in practice. There are many reasons for this, but in
hindsight it appears that it’s difficult to get people to accept a cryptocurrency with a centralized
authority.
Therefore, the central technical challenge that we need to solve in order to improve on ScroogeCoin
and create a workable system is: can we descroogify the system? That is, can we get rid of that
centralized Scrooge figure? Can we have a cryptocurrency that operates like ScroogeCoin in many
ways, but doesn’t have any central trusted authority?
To do that, we need to figure out how all users can agree upon a single published block chain as the
history of which transactions have happened. They must all agree on which transactions are valid, and
which transactions have actually occurred. They also need to be able to assign IDs to things in a
decentralized way. Finally, the minting of new coins needs to be controlled in a decentralized way. If
we can solve all of those problems, then we can build a currency that would be like ScroogeCoin but
without a centralized party. In fact, this would be a system very much like Bitcoin.

