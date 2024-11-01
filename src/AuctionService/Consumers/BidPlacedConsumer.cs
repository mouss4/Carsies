using AuctionService.Data;
using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDbContext _dbcontext;

        public BidPlacedConsumer(AuctionDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("--> Consuming bid placed");

            var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);

            if(auction.CurrentHignBid == null 
                || context.Message.BidStatus.Contains("Accepted") 
                && context.Message.Amount > auction.CurrentHignBid)
            {
                auction.CurrentHignBid = context.Message.Amount;
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
