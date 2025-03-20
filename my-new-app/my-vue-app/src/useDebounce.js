import { ref, watch } from 'vue';

export function useDebounce(value, delay = 300) {
  const debouncedValue = ref(value);

  watch(
    value,
    (newValue) => {
      const timer = setTimeout(() => {
        debouncedValue.value = newValue;
      }, delay);

      return () => clearTimeout(timer);
    },
    { immediate: true }
  );

  return debouncedValue;
}
