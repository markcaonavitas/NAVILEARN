class List extends Array
{
    static get [Symbol.species]() { return Array; }  // allow subclass of Array to use Array methods

    constructor(array = [])
    { //default to empty array
        super();
        var _this = this; //_this allows this to be exposed to constructor functions
        array.forEach((thing, index, array) =>
        {
            this.push(thing);
        });
    }

    FirstOrDefault(aFunction)
    {
        return this.find(aFunction);
        // var thing = null;
        // for (var i in this)
        // {
        //     if (aFunction(this[i]))
        //     {
        //         thing = this[i];
        //         break;
        //     }
        // };
        // return thing;
    }

    Where(aFunction)
    {
        var newList = new List();
        for (var i in this)
        {
            if (aFunction(this[i]))
            {
                newList.push(this[i]);
            }
        };
        return newList;
    }

    ToList()
    { //just to keep code looking like original c# implementation
        return this;
    }

    Add(thing)
    {
        this.push(thing);
    }
    Contains(thing)
    {
        return this.includes(thing);
    }
    get Count()
    { //just to keep code looking like original c# implementation
        return this.length;
    }
    GetRange(index, length)
    {
        return this.slice(index, length);
        // var newList = new List();
        // for (var i = index; i < index + length; i++)
        // {
        //     newList.push(this[i]);
        // };
        // return newList;

    }
    DeleteRange(index, length)
    {
        return this.splice(index, length)
    }
    Insert(index, thing)
    {
        this.splice(index, 0, thing)
    }
    Remove(uniqueID)
    {
        try
        {
            //crazy javascript splice always gererates undefined exception so do it this way
            const findIndex = this.findIndex(a => a.parentPage.uniqueID == uniqueID);
            return this.splice(findIndex, 1);
            // if (findIndex !== -1)
            // {
            //     var j = 0;
            //     for (var i = 0; i < this.length - 1 - j; i++)
            //     {//Search is skipped if length is only 1
            //         if (i == findIndex)
            //         {
            //             j += 1;
            //             this[i] = this[i + j]; //copy all but skip the one you want
            //         }
            //     }
            //     this.pop();
            // }
        }
        catch (e)
        {
            console.log("Still having remove issues? = " + e);
        }
    }
}
// JavaScript source code
