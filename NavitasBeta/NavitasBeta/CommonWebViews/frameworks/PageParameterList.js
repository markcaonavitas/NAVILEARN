class PageParameterList extends List {
    static ParameterListType = { TAC: 0, TSX: 1 };
    static ProtocalCommandTypes = { READ:0x20, WRITE: 0x21}; //protocal actually does something different but you get the idea.
    //fields declared here to be public
    Active = null;
    ProtocalCommandType = "";
    uniqueID = null;
    ParentTitle = "";
    parameterListType = null;
    parameterList = [];
    timeCreated = null;
    constructor(pageType, parentTitle) {
        super();
        var _this = this; //_this allows this to be exposed to constructor functions
        //fields declared here are private?
        this.Active = false;
        this.uniqueID = Math.random()*Date.now();
        this.ParentTitle = parentTitle;
        this.parameterListType = pageType;
        this.parameterList = new List();
        this.timeCreated = Date.now();
        //public PageParameterList() { } //somehow makes exposes it of typeof() so that I can serialize it?
    }
}
