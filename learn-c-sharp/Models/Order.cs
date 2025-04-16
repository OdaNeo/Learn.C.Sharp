using Stateless;
using System.ComponentModel.DataAnnotations;

namespace learn_c_sharp.Models
{
    public enum OrderStateEnum // 状态
    {
        Pending,
        Processing,
        Completed,
        Declined,
        Cancelled,
        Refund,
    }
    public enum OrderStateTriggerEnum //触发
    {
        PlaceOrder,
        Approve,
        Reject,
        Cancel,
        Return
    }
    public class Order
    {
        public Order()
        {
            StateMachineInti();
        }

        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<LineItem> OrderItems { get; set; }
        public OrderStateEnum State { get; set; }
        public DateTime CreateDateUTC { get; set; }
        public string? TransactionMetadata { get; set; }//第三方支付 json

        StateMachine<OrderStateEnum, OrderStateTriggerEnum> _machine;
        private void StateMachineInti()
        {
            _machine = new StateMachine<OrderStateEnum, OrderStateTriggerEnum>(OrderStateEnum.Pending);

            _machine.Configure(OrderStateEnum.Pending)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Cancel, OrderStateEnum.Cancelled);

            _machine.Configure(OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Approve, OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Reject, OrderStateEnum.Declined);

            _machine.Configure(OrderStateEnum.Declined)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing);

            _machine.Configure(OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Return, OrderStateEnum.Refund);
        }
    }
}
