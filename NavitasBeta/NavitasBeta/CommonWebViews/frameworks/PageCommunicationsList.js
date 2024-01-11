class PageCommunicationsList
{
    parentPage = []; //for reference to Active comm var
    parametersGroupedto64bytesAndAddressRange = [];
    matchingPackets = []; //public List<List<byte>>matchingPackets;
    constructor(parentPage, parametersGroupedto64bytesAndAddressRange, matchingPackets)
    {
        this.parentPage = parentPage;
        this.parametersGroupedto64bytesAndAddressRange = parametersGroupedto64bytesAndAddressRange;
        this.matchingPackets = matchingPackets;
    }
};