using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.Payment.Interface;

public interface IAliPay
{
    AlipayTradeWapPayResponse WapPay(string tradeNo, string subject, decimal amount, string return_url);

    AlipayTradeQueryResponse QueryPay(string tradeNo);

    AlipayTradeCloseResponse Close(string tradeNO);

    AlipayTradeRefundResponse Refund(string tradeNo, string refundId, decimal refundAmount, string reason);
}
