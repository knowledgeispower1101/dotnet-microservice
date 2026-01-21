import http from "k6/http";
import { check, sleep } from "k6";

export const options = {
  scenarios: {
    race: {
      executor: "constant-arrival-rate",
      rate: 50,
      timeUnit: "1s",
      duration: "1s",
      preAllocatedVUs: 50,
    },
  },
};

export default function () {
  const url =
    "http://localhost/api/ecommerce/inventory/7d89369b-dd4d-4212-8cca-c0f9b573ff37/release";

  const payload = JSON.stringify({
    quantity: 1,
  });

  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };

  const res = http.post(url, payload, params);

  check(res, {
    "status is 200 or 409": (r) => r.status === 200 || r.status === 409,
  });

  sleep(0.1);
}
