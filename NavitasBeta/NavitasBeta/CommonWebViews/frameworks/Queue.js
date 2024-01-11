class Queue extends Array {
    static get [Symbol.species]() { return Array; }  // allow subclass of Array to use Array methods

    constructor(array = []) { //default to empty array
        super();
        var _this = this; //_this allows this to be exposed to constructor functions
    }

    Enqueue(item) {
        this.push(item);
    }
    Dequeue(item) {
        return this.shift();
    }
    Clear(item) {
        this.splice(0, this.length);
    }
    get Count() {
        return this.length;
    }
}
