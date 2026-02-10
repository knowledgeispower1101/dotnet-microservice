export const STEPS = {
  CATEGORY: 0,
  LOCATION: 1,
  INFO: 2,
  IMAGE: 3,
  DESCRIPTION: 4,
  PRICE: 5,
} as const;

export type Steps = (typeof STEPS)[keyof typeof STEPS];
